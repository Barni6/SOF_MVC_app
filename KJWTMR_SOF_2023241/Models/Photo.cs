using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KJWTMR_SOF_2023241.Models
{
    public class Photo
    {
        static Random r = new Random();

        [Key]
        [StringLength(100)]
        public string Uid { get; set; }

        //[StringLength(100)]
        //public string Title { get; set; }

        public int Sequence { get; set; }

        public string UserId { get; set; }

        [NotMapped]
        public virtual IdentityUser User { get; set; }

        [StringLength(250)]
        public string PhotoUrl { get; set; }

        public Photo()
        {
            Uid = Guid.NewGuid().ToString();
            Sequence = r.Next(10, 1000);
        }
    }
}
