﻿using SuplementShopWEB.MVC.Application.ViewModel.AdminPanel.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuplementShopWEB.MVC.Application.Interfaces
{
    public interface IUserService
    {
        public ListUserVm GetUserVm();
    }
}