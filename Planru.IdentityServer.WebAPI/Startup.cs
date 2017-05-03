using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;
using Planru.Intrastructure.Identity.Models;
using Planru.Intrastructure.Identity.Data;

namespace Planru.IdentityServer.WebAPI
{
    public partial class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
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

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // this will do the initial DB population
            //InitializeDatabase(app);

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIdentity();

            app.UseIdentityServer();

            app.UseMvc();
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
        }
    }
}
