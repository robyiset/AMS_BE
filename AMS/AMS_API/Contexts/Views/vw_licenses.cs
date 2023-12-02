using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMS_API.Contexts.Views
{
    public class vw_licenses
    {
        [Key]
        public int id_license { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string license_name { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? license_version { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string? license_account { get; set; }
        [Column(TypeName = "varchar(-1)")]
        public string? license_desc { get; set; }
        public DateTime? purchase_date { get; set; }
        [Column(TypeName = "decimal(18,0)")]
        public decimal? purchase_cost { get; set; }
        public DateTime? expired_date { get; set; }
        public DateTime? termination_date { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string? asset_name { get; set; }
        [Column(TypeName = "varchar(201)")]
        public string? license_to_user { get; set; }

    }
}
