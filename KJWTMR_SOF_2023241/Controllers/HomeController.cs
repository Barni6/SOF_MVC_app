using KJWTMR_SOF_2023241.Data;
using KJWTMR_SOF_2023241.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KJWTMR_SOF_2023241.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<SiteUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(UserManager<SiteUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _db = db;
        }

        public async Task<IActionResult> DelegateAdmin()
        {
            var principal = this.User;
            var user = await _userManager.GetUserAsync(principal);
            var role = new IdentityRole()
            {
                Name = "Admin"
            };
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(role);
            }
            await _userManager.AddToRoleAsync(user, "Admin");
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return View(_db.Alcohols);
        }

        public IActionResult Index()
        {
            return View(_db.Alcohols);
        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(Alcohol alcohol)
        {
            alcohol.OwnerId = _userManager.GetUserId(this.User);

            var old = _db.Alcohols.FirstOrDefault(n => n.Name == alcohol.Name && n.OwnerId == alcohol.OwnerId);
            if (old == null)
            {
                _db.Alcohols.Add(alcohol);
                _db.SaveChanges();
            }
           
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(string uid)
        {
            var item = _db.Alcohols.FirstOrDefault(n => n.Uid == uid);
            if (item != null &&item.OwnerId == _userManager.GetUserId(this.User))
            {
                _db.Alcohols.Remove(item);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminDelete(string uid)
        {
            var item = _db.Alcohols.FirstOrDefault(n => n.Uid == uid);
            if (item != null)
            {
                _db.Alcohols.Remove(item);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }



        [Authorize]
        public async Task<IActionResult> Privacy()
        {
            var principal = this.User;
            var user = await _userManager.GetUserAsync(principal);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}