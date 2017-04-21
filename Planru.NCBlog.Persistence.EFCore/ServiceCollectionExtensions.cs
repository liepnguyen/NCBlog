using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Planru.NCBlog.Persistence.EFCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCloudscribeSimpleContentEFStorageCommon(this IServiceCollection services)
        {
            return services;
        }
    }
}
