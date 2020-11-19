using AutoMapper;
using Library.API.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Configurations
{
    public static class ContextMapperConfiguration
    {
        /// <summary>
        /// Adding context and mapper to DI container
        /// </summary>
        /// <param name="services"></param>
        /// <param name="appConfig">App configuration</param>
        public static void AddDbContextAndMapper(this IServiceCollection services, IConfiguration appConfig)
        {
            services.AddDbContext<LibraryContext>(configure => {
                configure.UseSqlServer(appConfig.GetConnectionString("LibraryConnection"));
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
