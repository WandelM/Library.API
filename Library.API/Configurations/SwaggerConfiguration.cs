using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Configurations
{
    public static class SwaggerConfiguration
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(setup =>
            {
                var apiInfo = new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "LibraryAPI",
                    Version = "v1"
                };

                setup.SwaggerDoc("LibraryAPI", apiInfo);
            });
        }
    }
}
