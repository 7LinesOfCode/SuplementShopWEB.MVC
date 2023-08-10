using AutoMapper;
using SuplementShopWEB.MVC.Application.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuplementShopWEB.MVC.Application.ViewModel.Customer
{
    public class CustomerForListVm : IMapFrom<SuplementShopWEB.MVC.Domain.Models.Customer>
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SuplementShopWEB.MVC.Domain.Models.Customer, CustomerForListVm>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.FirstName + " " + s.LastName))
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber)); 
        }

    }
}
