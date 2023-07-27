using SuplementShopWEB.MVC.Domain.Models;

namespace SuplementShopWEB.MVC.Application.ViewModel.Order
{
    public class OrderForListVm
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerFullName { get; set; }    
        public string PhoneNumber { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public double Price { get; set; }
        
    }
}
