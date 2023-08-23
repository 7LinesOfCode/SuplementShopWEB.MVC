using AutoMapper;
using FluentValidation;
using SuplementShopWEB.MVC.Application.Mapping;
using SuplementShopWEB.MVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuplementShopWEB.MVC.Application.ViewModel.Customer
{
    public class NewCustomerVm : IMapFrom<SuplementShopWEB.MVC.Domain.Models.Customer>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile) // Mapper For Post actions (Add Action)
        {
            profile.CreateMap<NewCustomerVm, SuplementShopWEB.MVC.Domain.Models.Customer>().ReverseMap();
        }

        public class NewCustomerValidator : AbstractValidator<NewCustomerVm>
        {
            public NewCustomerValidator()
            { 
                RuleFor(x => x.Id).NotNull();
                RuleFor(x => x.FirstName).MaximumLength(20);
                RuleFor(x => x.LastName).MaximumLength(20);
                RuleFor(x => x.EmailAddress).EmailAddress();
                RuleFor(x =>x.PhoneNumber).Length(9);
            }

        }

    }
}
