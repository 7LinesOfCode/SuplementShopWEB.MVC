﻿using SuplementShopWEB.MVC.Application.ViewModel.Item;
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
        public int AddItem(NewItemVm item);
        public NewItemVm GetItemByIdEdit(int id);
        public List<SuplementShopWEB.MVC.Domain.Models.Type> GetListOfTypes();
        public void UpdateItem(NewItemVm item);
    }
}
