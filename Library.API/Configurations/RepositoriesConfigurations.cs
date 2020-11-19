using Library.API.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Configurations
{
    public static class RepositoriesConfigurations
    {
        /// <summary>
        /// Adding repositories to DI container
        /// </summary>
        /// <param name="services">Services collection container</param>
        public static void AddRepositiories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IAuthorRepository), typeof(AuthorsRepository));

            services.AddTransient(typeof(IPublicationHouseRepository), typeof(PublicationHouseRepository));

            services.AddTransient(typeof(ICategoriesRepository), typeof(CategoryRepository));

            services.AddTransient(typeof(IPublicationRepository), typeof(PublicationRepository));

            services.AddTransient(typeof(IUsersRepository), typeof(UsersRepository));
        }
    }
}
