using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuplementShopWEB.MVC.Application.ViewModel.Item
{
    public class ListItemForListVm
    {
        public List<ItemForListVm> Items { get; set; }

        public bool IsAvailable { get; set; }
        public int CurrentlyPage { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public int Count { get; set; }
    }
}
