namespace SuplementShopWEB.MVC.Application.ViewModel.Item
{
    public class ItemForListVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public SuplementShopWEB.MVC.Domain.Models.Type ItemType { get; set; }
    }
}
