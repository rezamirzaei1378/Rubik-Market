using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Rubik_Market.Application.Services.Contracts;
using Rubik_Market.Application.Services.Implementation;
using Rubik_Market.Domain.Repo.Contracts;
using Rubik_Market.Infra.Data.Repo.Implementation;

namespace Rubik_Market.Infra.IOC
{
    public static class Di_Containers
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            #region Services

            services.AddScoped<IUserServices,UserService> ();
            services.AddScoped<IEmailSender,EmailSender> ();

            #endregion


            #region Repositories

            services.AddScoped<IUserRepository, UserRepository>();

            #endregion
        }
    }
}
