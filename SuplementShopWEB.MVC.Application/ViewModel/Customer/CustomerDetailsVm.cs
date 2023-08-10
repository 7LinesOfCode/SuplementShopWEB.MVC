using AutoMapper;
using SuplementShopWEB.MVC.Application.Mapping;
using SuplementShopWEB.MVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuplementShopWEB.MVC.Application.ViewModel.Customer
{
    public class CustomerDetailsVm  : IMapFrom<SuplementShopWEB.MVC.Domain.Models.Customer>
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public  ICollection<Domain.Models.Order> Orders { get; set; }

        public void Mapping (Profile profile) 
        { 
            profile.CreateMap<SuplementShopWEB.MVC.Domain.Models.Customer, CustomerDetailsVm>()
                .ForMember(s=>s.FullName, opt => opt.MapFrom(d=>d.FirstName + " " + d.LastName))
                .ForMember(s=>s.Orders,opt =>opt.Ignore());
        }
    }
}
