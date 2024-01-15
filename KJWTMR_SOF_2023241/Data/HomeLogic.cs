using KJWTMR_SOF_2023241.Controllers;
using KJWTMR_SOF_2023241.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Security.Claims;

namespace KJWTMR_SOF_2023241.Data
{
    public class HomeLogic : IHomeLogic
    {
        private readonly UserManager<SiteUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;


        public HomeLogic(UserManager<SiteUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        public IEnumerable<Alcohol> GetAlcohols()
        {
            return _db.Alcohols.ToList();
        }

        public void AddAlcohol(Alcohol alcohol, ClaimsPrincipal user)
        {
            alcohol.OwnerId = _userManager.GetUserId(user);
            var old = _db.Alcohols.FirstOrDefault(n => n.Name == alcohol.Name && n.OwnerId == alcohol.OwnerId);
            if (old == null)
            {
                _db.Alcohols.Add(alcohol);
                _db.SaveChanges();
            }
        }

        public void DeleteAlcohol(string uid, ClaimsPrincipal user)
        {
            var item = _db.Alcohols.FirstOrDefault(n => n.Uid == uid);
            if (item != null && item.OwnerId == _userManager.GetUserId(user))
            {
                _db.Alcohols.Remove(item);
                _db.SaveChanges();
            }
        }


        public async Task DelegateAdmin(ClaimsPrincipal user)
        {
            var siteUser = await _userManager.GetUserAsync(user);
            var role = new IdentityRole()
            {
                Name = "Admin"
            };
                 
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(role);
            }

            await _userManager.AddToRoleAsync(siteUser, "Admin");
        }

        public IEnumerable<SiteUser> GetUsers()
        {
            return _userManager.Users.ToList();
        }

        public async Task RemoveAdmin(string uid)
        {
            var user = _userManager.Users.FirstOrDefault(t => t.Id == uid);
            await _userManager.RemoveFromRoleAsync(user, "Admin");
        }

        public async Task GrantAdmin(string uid)
        {
            var user = _userManager.Users.FirstOrDefault(t => t.Id == uid);
            await _userManager.AddToRoleAsync(user, "Admin");
        }


        public void AdminDeleteAlcohol(string uid)
        {
            var item = _db.Alcohols.FirstOrDefault(n => n.Uid == uid);
            if (item != null)
            {
                _db.Alcohols.Remove(item);
                _db.SaveChanges();
            }
        }


        public async Task DoSomethingWithUser(ClaimsPrincipal user)
        {
            var siteUser = await _userManager.GetUserAsync(user);
            // Csinalj valamit a felhasznalovel
        }
    }
}
