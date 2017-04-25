using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Planru.NCBlog.Application.Interfaces;
using Planru.NCBlog.Application.Services;
using Planru.NCBlog.Domain.Persistence;
using Planru.NCBlog.Persistence.EFCore.MSSQL;
using Planru.NCBlog.Persistence.EFCore;

namespace Planru.NCBlog.IoC
{
    public class InjectorBootstrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ContentDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Application
            services.AddSingleton(Mapper.Configuration);
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService));
            services.AddScoped<IBlogAppService, BlogAppService>();

            // Persistence
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IContentDbContext, ContentDbContext>();
        }
    }
}
