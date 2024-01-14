using KJWTMR_SOF_2023241.Data;
using KJWTMR_SOF_2023241.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KJWTMR_SOF_2023241.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeLogic _homeLogic;

        public HomeController(IHomeLogic homeLogic)
        {
            _homeLogic = homeLogic;
        }

        
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult ListAlcohol()
        {
            return View(_homeLogic.GetAlcohols());
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
            var user = this.User;
            _homeLogic.AddAlcohol(alcohol, user);

            return RedirectToAction(nameof(ListAlcohol));
        }

        public async Task<IActionResult> DelegateAdmin()
        {
            await _homeLogic.DelegateAdmin(this.User);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return View(_homeLogic.GetAlcohols());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Users()
        {
            return View(_homeLogic.GetUsers());
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveAdmin(string uid)
        {
            await _homeLogic.RemoveAdmin(uid);
            return RedirectToAction(nameof(Users));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GrantAdmin(string uid)
        {
            await _homeLogic.GrantAdmin(uid);
            return RedirectToAction(nameof(Users));
        }

        public IActionResult Delete(string uid)
        {
            _homeLogic.DeleteAlcohol(uid, this.User);
            return RedirectToAction(nameof(ListAlcohol));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminDelete(string uid)
        {
            _homeLogic.AdminDeleteAlcohol(uid);
            return RedirectToAction(nameof(ListAlcohol));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Privacy()
        {
            await _homeLogic.DoSomethingWithUser(this.User);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
