using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Planru.NCBlog.Application.Interfaces;
using Planru.NCBlog.Application.Services;
using Planru.NCBlog.Domain.Persistence;
using Planru.NCBlog.Persistence.EFCore;
using Planru.NCBlog.Persistence.EFCore.MSSQL;
using System;

namespace Planru.NCBlog.IoC
{
    public class InjectorBootstrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Application
            services.AddSingleton(Mapper.Configuration);
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));
            services.AddScoped<IBlogAppService, BlogAppService>();

            // Persistence
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IContentDbContext, ContentDbContext>();
        }
    }
}
