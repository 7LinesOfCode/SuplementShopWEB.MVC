using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using SuplementShopWEB.MVC.Domain.Interface;
using SuplementShopWEB.MVC.Domain.Models;
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

        public IdentityUser GetUserByEmail(string email)
        {
            email = email.ToUpper();
            return _context.Users.FirstOrDefault(u => u.NormalizedEmail == email);
        }

        public IdentityUser GetUserById(string Id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == Id);
        }

        public IQueryable<IdentityUser> GetUsers()
        {
            return _context.Users;
        }

        public List<IdentityUser> GetAdmins() 
        {
            var userIds = _context.UserRoles
                    .Where(ur => ur.RoleId == "Admin")
                    .Select(ur => ur.UserId)
                    .ToList();

            var adminUsers = _context.Users
                    .Where(u => userIds.Contains(u.Id))
                    .ToList();

            return adminUsers;
        }

        public void restrictPermissions(string id)
        {
            var userRoleToRemove = _context.UserRoles.FirstOrDefault(ur => ur.UserId == id);
            if (userRoleToRemove != null)
            {
                _context.UserRoles.Remove(userRoleToRemove);
                _context.SaveChanges();
            }
        }

        public void addPermissions(string Email)
        {
            var user = GetUserByEmail(Email);

            if (user != null)
            {

                IdentityUserRole<string> newAdmin = new IdentityUserRole<string>
                {
                    UserId = user.Id,
                    RoleId = "Admin"
                };
                _context.UserRoles.Add(newAdmin);
                _context.SaveChanges();
            }
        }

        public void deleteUser(string id)
        {
            var userToDelete = GetUserById(id);
            _context.Users.Remove(userToDelete);
            _context.SaveChanges();
        }
    }

}
