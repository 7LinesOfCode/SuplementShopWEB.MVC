using Microsoft.Extensions.DependencyInjection;
using SuplementShopWEB.MVC.Domain.Interface;
using SuplementShopWEB.MVC.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuplementShopWEB.MVC.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IOrderRepository,OrderRepository>();

            return services;
        }
    }
}
