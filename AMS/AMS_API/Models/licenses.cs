using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMS_API.Models
{
    public class licenses
    {
    }
    public class add_license
    {
        [Required(ErrorMessage = "Name is required")]
        public string license_name { get; set; }
        public string? license_desc { get; set; }
        public string? license_account { get; set; }
        public string? license_version { get; set; }
        public int? id_user { get; set; }
        public int? id_asset { get; set; }
        public DateTime? purchase_date { get; set; }
        public decimal? purchase_cost { get; set; }
        public DateTime? expired_date { get; set; }
        public DateTime? termination_date { get; set; }
    }
    public class update_license
    {
        [Required(ErrorMessage = "Id is required")]
        public int id_license { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string license_name { get; set; }
        public string? license_desc { get; set; }
        public string? license_account { get; set; }
        public string? license_version { get; set; }
        public int? id_user { get; set; }
        public int? id_asset { get; set; }
        public DateTime? purchase_date { get; set; }
        public decimal? purchase_cost { get; set; }
        public DateTime? expired_date { get; set; }
        public DateTime? termination_date { get; set; }
    }
}
