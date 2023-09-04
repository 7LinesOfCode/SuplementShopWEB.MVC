using AutoMapper;
using AutoMapper.QueryableExtensions;
using SuplementShopWEB.MVC.Application.Interfaces;
using SuplementShopWEB.MVC.Application.ViewModel.Item;
using SuplementShopWEB.MVC.Domain.Interface;
using SuplementShopWEB.MVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuplementShopWEB.MVC.Application.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _repo;
        private readonly IMapper _mapper;

        public ItemService(IItemRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public int AddItem(NewItemVm item)
        {
            var newItem = _mapper.Map<Item>(item);
            var id = _repo.AddItem(newItem);
            return id;
        }

        public void DeleteItem(int id)
        {
            _repo.DeleteItem(id);
        } // DONE

        public ListItemForListVm GetAllItems(int pageSize, int pageNo, string searchString, bool IsAvailable) /// WORK
        {
            var items = _repo
                .GetAllItems()
                .Where(c => (c.Name).Contains(searchString))
                .Where(c =>(c.IsAvailable == IsAvailable))
                .ProjectTo<ItemForListVm>(_mapper.ConfigurationProvider)
                .ToList();

            var itemsToShow = items.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToList();

            var itemList = new ListItemForListVm()
            {
                PageSize = pageSize,
                CurrentlyPage = pageNo,
                SearchString = searchString,
                Items = itemsToShow,
                Count = items.Count
            };
            return itemList;
        } // DONE

        public ListTypeForListVm GetAllTypes()
        {
            var types = _repo
                .GetAllTypes()
                .ProjectTo<TypeForListVm>(_mapper.ConfigurationProvider)
                .ToList();
            var typeList = new ListTypeForListVm()
            {
                Types = types,
                Count = types.Count
            };
            return typeList;
        } // DONE



        public List<SuplementShopWEB.MVC.Domain.Models.Type> GetListOfTypes()
        {
            var types = _repo.GetAllTypes().ToList();
            return types;
        }

        public ListItemForListVm GetAvailableItemsOnly() /// WORK
        {
            var items = _repo
                .GetAllItems()
                .Where(i => i.IsAvailable)
                .ProjectTo<ItemForListVm>(_mapper.ConfigurationProvider)
                .ToList();

            var itemList = new ListItemForListVm()
            {
                Items = items,
                Count = items.Count
            };
            return itemList;
        } // DONE

        public int GetCountOfItemById(int id)
        {
            var result = _repo.GetCountOfItem(id);
            return result;
        } // DONE

        public ItemDetailsVm GetItemById(int id)
        {
            var item = _repo.GetItemById(id);
            var itemVm = _mapper.Map<ItemDetailsVm>(item);
            return itemVm;
        }// DONE



        public NewItemVm GetItemByIdEdit(int id)
        {
            var item = _repo.GetItemById(id);
            var NewItemVm = _mapper.Map<NewItemVm>(item);
            return NewItemVm;
        }

        public ListItemForListVm GetItemsByType(string type)
        {
            var items = _repo
                .GetItemsByType(type)
                .ProjectTo<ItemForListVm>(_mapper.ConfigurationProvider)
                .ToList();

            var itemList = new ListItemForListVm()
            {
                Items = items,
                Count = items.Count
            };
            return itemList;
        }// DONE

        public void UpdateItem(NewItemVm item)
        {
            var EditedItem = _mapper.Map<Item>(item);
            _repo.UpdateItem(EditedItem);
        }

        public string GetTypeNameByTypeId(int id)
        {
            return _repo.GetTypeById(id);
        }

    }
}
