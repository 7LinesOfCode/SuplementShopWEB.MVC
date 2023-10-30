using SuplementShopWEB.MVC.Application.Interfaces;
using SuplementShopWEB.MVC.Application.ViewModel.AdminPanel.Users;
using SuplementShopWEB.MVC.Application.ViewModel.Item;
using SuplementShopWEB.MVC.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuplementShopWEB.MVC.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        public UserService(IUserRepository repository)
        {
            _repo= repository;
        }

        public ListUserVm GetUserVm()
        {
           ListUserVm list = new ListUserVm();
            list.Users = new List<UserVm>();
            var users = _repo.GetUsers();
            foreach (var user in users)
            {
                UserVm uservm = new UserVm();
                uservm.Id = user.Id;
                uservm.Email = user.Email;
                list.Users.Add(uservm);
            }
            return list;
            
        }

        public ListUserVm GetAdmins()
        {
            ListUserVm list = new ListUserVm();
            list.Users = new List<UserVm>();
            var admins = _repo.GetAdmins();
            foreach (var user in admins)
            {
                UserVm uservm = new UserVm();
                uservm.Id = user.Id;
                uservm.Email = user.Email;
                uservm.Role = "Admin";
                list.Users.Add(uservm);
            }
            return list;
        }

        public void restrictPermissions(string id)
        {
            if(id != null)
            {
                _repo.restrictPermissions(id);
            }
        }
    }
}
