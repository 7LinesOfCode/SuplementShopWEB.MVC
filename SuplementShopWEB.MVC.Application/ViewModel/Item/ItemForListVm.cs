using AutoMapper;
using SuplementShopWEB.MVC.Application.Mapping;
using SuplementShopWEB.MVC.Application.ViewModel.Customer;

namespace SuplementShopWEB.MVC.Application.ViewModel.Item
{
    public class ItemForListVm : IMapFrom<SuplementShopWEB.MVC.Domain.Models.Item>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public bool IsAvailable { get; set; }
        public SuplementShopWEB.MVC.Domain.Models.Type Type { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SuplementShopWEB.MVC.Domain.Models.Item, ItemForListVm>();
        }
    }
}
