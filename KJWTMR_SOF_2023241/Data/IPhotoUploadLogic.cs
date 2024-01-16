using KJWTMR_SOF_2023241.Models;
using System.Security.Claims;

namespace KJWTMR_SOF_2023241.Data
{
    public interface IPhotoUploadLogic
    {
        IEnumerable<Photo> GetPhotos();
        void AddPhoto(Photo photo, IFormFile photoUpload, ClaimsPrincipal user);
        //byte[] GetPhotoData(string Uid);
    }
}
