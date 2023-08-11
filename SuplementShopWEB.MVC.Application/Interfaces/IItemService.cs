using SuplementShopWEB.MVC.Application.ViewModel.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuplementShopWEB.MVC.Application.Interfaces
{
    public interface IItemService
    {
        public ListTypeForListVm GetAllTypes();
        public ListItemForListVm GetAllItems();
        public ItemDetailsVm GetItemById(int id);
        public ListItemForListVm GetItemsByType(string type);
        public ListItemForListVm GetAvailableItemsOnly();
        public int GetCountOfItemById(int id);
        public void DeleteItem(int id);
        public void AddItem();
    }
}
