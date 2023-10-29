using Microsoft.AspNetCore.Identity;
using SuplementShopWEB.MVC.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuplementShopWEB.MVC.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SuplementShopDbContext _context;
        public UserRepository(SuplementShopDbContext context)
        {
            _context = context;
        }

        public IEnumerable<IdentityUser> GetUsers()
        {
            return _context.Users.ToList();
        }
    }

}
