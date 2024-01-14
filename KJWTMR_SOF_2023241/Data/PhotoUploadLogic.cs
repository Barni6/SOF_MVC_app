using KJWTMR_SOF_2023241.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace KJWTMR_SOF_2023241.Data
{
    public class PhotoUploadLogic : IPhotoUploadLogic
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<SiteUser> _userManager;

        public PhotoUploadLogic(ApplicationDbContext db, UserManager<SiteUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IEnumerable<Photo> GetPhotos()
        {
            return _db.Photos;
        }

        public void AddPhoto(Photo photo, IFormFile photoUpload, ClaimsPrincipal user)
        {
            photo.UserId = _userManager.GetUserId(user);
            photo.ConetntType = photoUpload.ContentType;
            photo.PhotoData = new byte[photoUpload.Length];
            using (var stream = photoUpload.OpenReadStream())
            {
                stream.Read(photo.PhotoData, 0, photo.PhotoData.Length);
            }
            _db.Photos.Add(photo);
            _db.SaveChanges();
        }

        public byte[] GetPhotoData(string Uid)
        {
            var photo = _db.Photos.FirstOrDefault(t => t.Uid == Uid);
            return photo?.PhotoData;
        }
    }
}
