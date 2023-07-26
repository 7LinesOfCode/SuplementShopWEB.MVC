using SuplementShopWEB.MVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuplementShopWEB.MVC.Domain.Interface
{
    public interface IItemRepository
    {
        public void DeleteItem(int itemId);
        public IQueryable<Item> GetAllItems();
        public Item GetItemById(int itemId);
        public IQueryable<Item> GetItemsByTypeId(int typeId);
        public int AddItem(Item item);
        public IQueryable<SuplementShopWEB.MVC.Domain.Models.Type> GetAllTypes();
        public IQueryable<Item> GetItemsByType(string typeName);

    }
}
