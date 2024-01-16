using Azure.Storage.Blobs;
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
        private readonly IPhotoUploadLogic _photoUploadLogic;


        public PhotoUploadController(ILogger<HomeController> logger, IPhotoUploadLogic photoUploadLogic)
        {
            _logger = logger;
            _photoUploadLogic = photoUploadLogic;
        }

        [Authorize]
        public IActionResult ListPhoto()
        {
            var photos = _photoUploadLogic.GetPhotos();
            return View(photos);
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
            var user = this.User;
            _photoUploadLogic.AddPhoto(p, photoUpload, user);
            return RedirectToAction(nameof(ListPhoto));
        }

        //public IActionResult GetImage(string Uid)
        //{
        //    var photoData = _photoUploadLogic.GetPhotoData(Uid);
        //    if (photoData != null)
        //    {
        //        return new FileContentResult(photoData, "image/jpeg"); // Módosítsd a tartalom típusát az alkalmazkodóan a valóságos típushoz
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}
    }
}
