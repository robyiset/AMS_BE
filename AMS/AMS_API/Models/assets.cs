using System.ComponentModel.DataAnnotations;

namespace AMS_API.Models
{
    public class assets
    {
        public int id_asset { get; set; }
        [Required(ErrorMessage = "Serial is required")]
        public string serial { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string asset_name { get; set; }
        public string asset_desc { get; set; }
        public int? id_type { get; set; }
        public int? id_user { get; set; }
        public DateTime? purchase_date { get; set; }
        public decimal? purchase_cost { get; set; }
        public bool depreciate { get; set; }
        public bool requestable { get; set; }
        public bool consumable { get; set; }
        public int? id_location { get; set; }
        public int? id_company { get; set; }
        public int? id_warranty { get; set; }
        public string status { get; set; }

    }
}
