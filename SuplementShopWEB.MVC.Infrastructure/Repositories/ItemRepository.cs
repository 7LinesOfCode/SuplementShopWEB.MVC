using SuplementShopWEB.MVC.Domain.Interface;
using SuplementShopWEB.MVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuplementShopWEB.MVC.Infrastructure.Repositories
{
    public class ItemRepository : IItemRepository

    {
        private readonly SuplementShopDbContext _context;
        public ItemRepository(SuplementShopDbContext context)
        {
            _context = context; 
        }

        public void DeleteItem(int itemId)
        {
            var item = _context.Items.Find(itemId);
            if (item is not null) 
            {
                _context.Items.Remove(item);
                _context.SaveChanges();
            }
        }

        public IQueryable<Item> GetAllItems()
        {
            var items = _context.Items;
            return items;
        }

        public Item GetItemById (int itemId) 
        {
            var item = _context.Items.FirstOrDefault(i => i.Id == itemId);
            return item;
        }

        public IQueryable<Item> GetItemsByTypeId(int typeId) 
        {
            var items = _context.Items.Where(i => i.TypeId == typeId);
            return items;
        }

        public int AddItem(Item item)
        {
            _context.Items.Add(item);
            _context.SaveChanges();
            return item.Id;
        }

        public IQueryable<Item> GetItemsByType(string typeName)
        {
            var items = _context.Items.Where(i => i.Type.Name == typeName);
            return items;   
        }
        public int GetCountOfItem(int id)
        {
            var item = _context.Items
                .SingleOrDefault(i =>i.Id == id);
            var result = item.Count;
            return result; 
        }


        public void UpdateItem(Item item)
        {
            _context.Items.Attach(item);
            _context.Entry(item).Property("Name").IsModified = true;
            _context.Entry(item).Property("Price").IsModified = true;
            _context.Entry(item).Property("Description").IsModified = true;
            _context.Entry(item).Property("TypeId").IsModified = true;
            _context.Entry(item).Property("Count").IsModified = true;
            _context.Entry(item).Property("IsAvailable").IsModified = true;
            _context.Entry(item).Property("IsAvailable").IsModified = true;

            _context.SaveChanges();
        }

        public IQueryable<SuplementShopWEB.MVC.Domain.Models.Type> GetAllTypes()
        {
            var types = _context.Types;
            return types;
        }


        public int AddType(Domain.Models.Type type)
        {
            _context.Types.Add(type);
            _context.SaveChanges();
            return type.Id;
        }

        public void DeleteType(int typeId)
        {
            var type = _context.Items.Find(typeId);
            if (type is not null)
            {
                _context.Items.Remove(type);
                _context.SaveChanges();
            }
        }
    }
}
