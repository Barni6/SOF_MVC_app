using KJWTMR_SOF_2023241.Data;
using KJWTMR_SOF_2023241.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.Diagnostics;

namespace KJWTMR_SOF_2023241.Controllers
{
    public class PhotoUploadController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly UserManager<SiteUser> _userManager;

        public PhotoUploadController(ILogger<HomeController> logger, ApplicationDbContext db, UserManager<SiteUser> userManager)
        {
            _logger = logger;
            _db = db;
            _userManager = userManager;
        }

        public IActionResult ListPhoto()
        {
            return View(_db.Photos);
        }

        [Authorize]
        public IActionResult AddPhoto()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddPhoto([FromForm] Photo p, [FromForm] IFormFile photoUpload)
        {
            p.UserId = _userManager.GetUserId(this.User);
            p.ConetntType = photoUpload.ContentType;
            p.PhotoData = new byte[photoUpload.Length];
            using(var stream = photoUpload.OpenReadStream())
            {
                stream.Read(p.PhotoData, 0, p.PhotoData.Length);
            }
            _db.Photos.Add(p);
            _db.SaveChanges();
            return RedirectToAction(nameof(ListPhoto));
        }

        public IActionResult GetImage(string Uid)
        {
            var photo = _db.Photos.FirstOrDefault(t => t.Uid == Uid);
            if (photo.ConetntType.Length > 3)
            {
                return new FileContentResult(photo.PhotoData, photo.ConetntType);
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
