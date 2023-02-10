using System.ComponentModel.DataAnnotations;

namespace secretSanta.Models
{
    public class playerDetails
    {
        [Required]
        public string name { get; set; }

        [Required]
        public string email { get; set; }
    }
}
