using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SuplementShopWEB.MVC.Application.Interfaces;
using SuplementShopWEB.MVC.Application.Services;
using SuplementShopWEB.MVC.Application.ViewModel.Customer;
using SuplementShopWEB.MVC.Domain.Interface;
using System;
using System.Collections.Generic; 
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static SuplementShopWEB.MVC.Application.ViewModel.Customer.NewCustomerVm;

namespace SuplementShopWEB.MVC.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAplication(this IServiceCollection services)
        {
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IItemService, ItemService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IValidator<NewCustomerVm>, NewCustomerValidator>();
            return services;
        }
    }
}
