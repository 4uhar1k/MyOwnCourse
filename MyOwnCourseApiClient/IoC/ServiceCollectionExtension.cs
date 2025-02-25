using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MyOwnCourseApiClient.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOwnCourseApiClient.IoC
{
    public static class ServiceCollectionExtension
    {
        public static void AddMOCApiClientService(this IServiceCollection services,
            Action<ApiClientOptions> options)
        {
            services.Configure(options);
            services.AddSingleton(provider =>
            {
                var options = provider.GetRequiredService<IOptions<ApiClientOptions>>().Value;
                return new MOCApiClientService(options);
            });
        }
    }
}
