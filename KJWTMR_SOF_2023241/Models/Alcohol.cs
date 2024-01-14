using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KJWTMR_SOF_2023241.Models
{
    public class Alcohol
    {
        [Key]
        public string Uid { get; set; }
        public string Name { get; set; }
        public string OwnerId { get; set; }

        [NotMapped]
        public virtual SiteUser Owner { get; set; }

        public Alcohol()
        {
            Uid = Guid.NewGuid().ToString();
            //OwnerId = Guid.NewGuid().ToString();
            //Owner = new SiteUser();
        }
    }
}
