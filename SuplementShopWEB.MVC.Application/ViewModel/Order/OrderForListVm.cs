using AutoMapper;
using SuplementShopWEB.MVC.Application.Mapping;
using SuplementShopWEB.MVC.Domain.Models;

namespace SuplementShopWEB.MVC.Application.ViewModel.Order
{
    public class OrderForListVm : IMapFrom<SuplementShopWEB.MVC.Domain.Models.Order>
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerFullName { get; set; }    
        public string PhoneNumber { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public double Price { get; set; }
        public int CountOfItems { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SuplementShopWEB.MVC.Domain.Models.Order, OrderForListVm>()
                .ForMember(s => s.CustomerFullName, opt => opt.MapFrom(d => d.Customer.FirstName + " " + d.Customer.LastName));
        }
    }
}
