using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuplementShopWEB.MVC.Domain.Interface
{
    public interface IUserRepository
    {
        public IQueryable<IdentityUser> GetUsers();
        public IdentityUser GetUserByEmail(string email);
        public IdentityUser GetUserById(string Id);
        public List<IdentityUser> GetAdmins();


    }
}
