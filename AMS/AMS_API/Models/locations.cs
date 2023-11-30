using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMS_API.Models
{
    public class locations
    {
        public int id_location { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string zip { get; set; }
        public int id_user { get; set; }
    }
}
