using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Planru.NCBlog.IoC;
using Planru.NCBlog.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;
using Planru.CrossCutting.Identity.Models;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.IdentityModel.Tokens;
using Planru.CrossCutting.Identity.Web.Policies;
using Planru.CrossCutting.Identity.Core;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Planru.CrossCutting.Identity.Web;

namespace Planru.NCMS.WebAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ContentDbContext>(options =>
            {
                options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"], b => b.MigrationsAssembly("Planru.NCMS.WebAPI"));
                options.UseOpenIddict();
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ContentDbContext>()
                .AddDefaultTokenProviders();

            // Configure Identity options and password complexity here
            services.Configure<IdentityOptions>(options =>
            {
                // User settings
                options.User.RequireUniqueEmail = true;

                //    //// Password settings
                //    //options.Password.RequireDigit = true;
                //    //options.Password.RequiredLength = 8;
                //    //options.Password.RequireNonAlphanumeric = false;
                //    //options.Password.RequireUppercase = true;
                //    //options.Password.RequireLowercase = false;

                //    //// Lockout settings
                //    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                //    //options.Lockout.MaxFailedAccessAttempts = 10;

                options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
            });

            // Register the OpenIddict services.
            services.AddOpenIddict(options =>
            {
                options.AddEntityFrameworkCoreStores<ContentDbContext>();
                options.AddMvcBinders();
                options.EnableTokenEndpoint("/connect/token");
                options.AllowPasswordFlow();
                options.AllowRefreshTokenFlow();
                options.DisableHttpsRequirement();
                // options.UseJsonWebTokens(); //Use JWT if preferred
                options.AddSigningKey(new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(Configuration["STSKey"])));
            });

            // Enable cors if required
            //services.AddCors();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthPolicies.ViewUserByUserIdPolicy, policy => policy.Requirements.Add(new ViewUserByIdRequirement()));

                options.AddPolicy(AuthPolicies.ViewUsersPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, ApplicationPermissions.ViewUsers));

                options.AddPolicy(AuthPolicies.ManageUserByUserIdPolicy, policy => policy.Requirements.Add(new ManageUserByIdRequirement()));

                options.AddPolicy(AuthPolicies.ManageUsersPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageUsers));

                options.AddPolicy(AuthPolicies.ViewRoleByRoleNamePolicy, policy => policy.Requirements.Add(new ViewRoleByNameRequirement()));

                options.AddPolicy(AuthPolicies.ViewRolesPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, ApplicationPermissions.ViewRoles));

                options.AddPolicy(AuthPolicies.AssignRolesPolicy, policy => policy.Requirements.Add(new AssignRolesRequirement()));

                options.AddPolicy(AuthPolicies.ManageRolesPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageRoles));
            });

            // Auth Policies
            services.AddSingleton<IAuthorizationHandler, ViewUserByIdHandler>();
            services.AddSingleton<IAuthorizationHandler, ManageUserByIdHandler>();
            services.AddSingleton<IAuthorizationHandler, ViewRoleByNameHandler>();
            services.AddSingleton<IAuthorizationHandler, AssignRolesHandler>();

            InjectorBootstrapper.RegisterServices(services, Configuration);

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<IdentityAutoMapperProfile>();
            });

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IDatabaseInitializer databaseInitializer)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseOAuthValidation();
            app.UseOpenIddict();

            app.UseMvc();

            try
            {
                databaseInitializer.SeedAsync().Wait();
            }
            catch
            {
                throw;
            }
        }
    }
}
