using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using KJWTMR_SOF_2023241.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace KJWTMR_SOF_2023241.Data
{
    public class PhotoUploadLogic : IPhotoUploadLogic
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<SiteUser> _userManager;

        BlobServiceClient serviceClient;
        BlobContainerClient containerClient;

        public PhotoUploadLogic(ApplicationDbContext db, UserManager<SiteUser> userManager)
        {
            _db = db;
            _userManager = userManager;

            serviceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=mynewyear;AccountKey=qJhD0i8DNPL+mLx8l64j+BoYwLkgG+NXcgNLc2q9JLkEhnzJCPWrw5lLTQTr5DMaVbwwzZM84naR+ASt4pgudg==;EndpointSuffix=core.windows.net");
            containerClient = serviceClient.GetBlobContainerClient("photos");
        }

        public IEnumerable<Photo> GetPhotos()
        {
            return _db.Photos;
        }

        public void AddPhoto(Photo photo, IFormFile photoUpload, ClaimsPrincipal user)
        {
            //photo.UserId = _userManager.GetUserId(user);
            //photo.ConetntType = photoUpload.ContentType;
            //photo.PhotoData = new byte[photoUpload.Length];
            //using (var stream = photoUpload.OpenReadStream())
            //{
            //    stream.Read(photo.PhotoData, 0, photo.PhotoData.Length);
            //}
            //_db.Photos.Add(photo);
            //_db.SaveChanges();

            photo.UserId = _userManager.GetUserId(user);

            BlobClient blobClient = containerClient.GetBlobClient(photo.UserId + "_" + photo.Uid.Replace(" ", "").ToLower());
            using (var uploadFileStream = photoUpload.OpenReadStream())
            {
                blobClient.Upload(uploadFileStream, true);
            }
            blobClient.SetAccessTier(AccessTier.Cool);
            photo.PhotoUrl = blobClient.Uri.AbsoluteUri;

            _db.Photos.Add(photo);
            _db.SaveChanges();
        }

        //public byte[] GetPhotoData(string Uid)
        //{
        //    var photo = _db.Photos.FirstOrDefault(t => t.Uid == Uid);
        //    return photo?.PhotoData;
        //}
    }
}
