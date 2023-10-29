using SuplementShopWEB.MVC.Application.ViewModel.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuplementShopWEB.MVC.Application.ViewModel.AdminPanel.Users
{
    public class ListUserVm
    {
        public List<UserVm> Users { get; set; }
        public int Count { get; set; }
    }
}
