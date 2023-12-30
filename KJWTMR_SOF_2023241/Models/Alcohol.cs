using System.ComponentModel.DataAnnotations;

namespace KJWTMR_SOF_2023241.Models
{
    public class Alcohol
    {
        [Key]
        public string Uid { get; set; }
        public string Name { get; set; }
        public string OwnerId { get; set; }

        public Alcohol()
        {
            Uid = Guid.NewGuid().ToString();
        }
    }
}
