using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.API.DbContexts;
using Library.API.Services;
using Library.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore;
using Microsoft.Extensions.Options;


namespace Library.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
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

            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("LibraryAPI", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "LibraryAPI",
                    Version = "v1"
                });
            });

            services.AddDbContext<LibraryContext>(configure => {
                configure.UseSqlServer(Configuration.GetConnectionString("LibraryConnection"));
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddTransient(typeof(IAuthorRepository), typeof(AuthorsRepository));

            services.AddTransient(typeof(IPublicationHouseRepository), typeof(PublicationHouseRepository));

            services.AddTransient(typeof(ICategoriesRepository), typeof(CategoryRepository));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment webHostEnvironment)
        {
            app.UseHttpsRedirection();
            
            app.UseRouting();

            app.UseEndpoints(configure =>
            {
                configure.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(setup =>
            {
                setup.SwaggerEndpoint("/swagger/LibraryAPI/swagger.json", "LibraryApi");
            });
        }
    }
}
