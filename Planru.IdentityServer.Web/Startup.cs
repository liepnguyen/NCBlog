using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;
using Planru.Intrastructure.Identity.Models;
using Planru.Intrastructure.Identity.Data;
using Planru.Intrastructure.Identity.Services;
using Microsoft.AspNetCore.Identity;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;

namespace Planru.IdentityServer.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            DefaultConnectionString = Configuration.GetConnectionString("DefaultConnection");
            MigrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
        }

        public string MigrationsAssembly { get; }
        public string DefaultConnectionString { get; }
        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<AuthenticationDbContext>(options =>
                options.UseSqlServer(DefaultConnectionString, sqlServerOptions => sqlServerOptions.MigrationsAssembly(MigrationsAssembly)));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AuthenticationDbContext>()
                .AddDefaultTokenProviders();

            // configure identity server with in-memory users, but EF stores for clients and scopes
            services.AddIdentityServer()
                .AddTemporarySigningCredential() // used for testing until a real cert is available
                .AddConfigurationStore(option =>
                    option.UseSqlServer(DefaultConnectionString, sqlServerOptions => sqlServerOptions.MigrationsAssembly(MigrationsAssembly)))
                .AddOperationalStore(option =>
                    option.UseSqlServer(DefaultConnectionString, sqlServerOptions => sqlServerOptions.MigrationsAssembly(MigrationsAssembly)))
                .AddAspNetIdentity<ApplicationUser>();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // this will do the initial DB population
            InitializeDatabase(app);
            InitializeRoles(app).Wait();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseIdentityServer();

            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var authenticationDbContext = scope.ServiceProvider.GetRequiredService<AuthenticationDbContext>();
                var persistedGrantDbContext = scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();
                var configurationDbContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

                authenticationDbContext.Database.Migrate();
                persistedGrantDbContext.Database.Migrate();
                configurationDbContext.Database.Migrate();

                if (!configurationDbContext.Clients.Any())
                {
                    foreach (var client in Config.GetClients())
                    {
                        configurationDbContext.Clients.Add(client.ToEntity());
                    }
                    configurationDbContext.SaveChanges();
                }

                if (!configurationDbContext.IdentityResources.Any())
                {
                    foreach (var resource in Config.GetIdentityResources())
                    {
                        configurationDbContext.IdentityResources.Add(resource.ToEntity());
                    }
                    configurationDbContext.SaveChanges();
                }

                if (!configurationDbContext.ApiResources.Any())
                {
                    foreach (var resource in Config.GetApiResources())
                    {
                        configurationDbContext.ApiResources.Add(resource.ToEntity());
                    }
                    configurationDbContext.SaveChanges();
                }
            }
        }

        // Initialize some test roles. In the real world, these would be setup explicitly by a role manager
        private string[] roles = new[] { "User", "Manager", "Administrator" };
        private async Task InitializeRoles(IApplicationBuilder app)
        {
            var roleManager = app.ApplicationServices.GetService<RoleManager<IdentityRole>>();
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var newRole = new IdentityRole(role);
                    await roleManager.CreateAsync(newRole);
                    // In the real world, there might be claims associated with roles
                    // await roleManager.AddClaimAsync(newRole, new Claim("foo", "bar"))
                }
            }
        }
    }
}
