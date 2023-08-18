using AutoMapper;
using SuplementShopWEB.MVC.Application.Mapping;
using SuplementShopWEB.MVC.Application.ViewModel.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuplementShopWEB.MVC.Application.ViewModel.Order
{
    public class NewOrderVm : IMapFrom<SuplementShopWEB.MVC.Domain.Models.Order>
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerFullName { get; set; }
        public string PhoneNumber { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public double Price { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SuplementShopWEB.MVC.Domain.Models.Order, NewOrderVm>()
                .ForMember(s => s.CustomerFullName, opt => opt.MapFrom(d => d.Customer.FirstName + " " + d.Customer.LastName));
        }
    }
}
