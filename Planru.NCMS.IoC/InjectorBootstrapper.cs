using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Planru.CrossCutting.Identity.Core;
using Planru.CrossCutting.Identity.Core.Interfaces;
using Planru.CrossCutting.Identity.Models;
using Planru.NCBlog.Persistence.EFCore;

namespace Planru.NCBlog.IoC
{
    public class InjectorBootstrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAccountManager, AccountManager>((serviceProvider) => 
            {
                return new AccountManager(
                    serviceProvider.GetService<ContentDbContext>(),
                    serviceProvider.GetService<UserManager<ApplicationUser>>(),
                    serviceProvider.GetService<RoleManager<ApplicationRole>>()); 
            });

            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();

            // Application
            //services.AddSingleton(Mapper.Configuration);
            //services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService));
            //services.AddScoped<IBlogAppService, BlogAppService>();

            // Persistence
            //services.AddScoped<IBlogRepository, BlogRepository>();
            //services.AddScoped<IContentDbContext, ContentDbContext>();
        }
    }
}
