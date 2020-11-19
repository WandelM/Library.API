using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Configurations
{
    public static class ControllersAndLogingConfiguration
    {
        public static void AddControllersAndLogging(this IServiceCollection services)
        {
            services.AddControllers(configure =>
            {
                configure.ReturnHttpNotAcceptable = true;
            })
                .AddNewtonsoftJson(configure =>
                {
                    configure.SerializerSettings.ContractResolver =
                    new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                });

            services.AddLogging(loggingConfiguration =>
            {
                loggingConfiguration.AddConsole();
            });
        }
    }
}
