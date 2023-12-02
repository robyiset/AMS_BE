using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMS_API.Contexts.Views
{
    public class vw_request_assets
    {
        [Key]
        public int? id_request { get; set; }
        public int id_asset { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string asset_name { get; set; }
        [Column(TypeName = "varchar(201)")]
        public string? requested_from_user { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string? requested_from_company { get; set; }
        public DateTime? requested_at { get; set; }
        public DateTime? denied_at { get; set; }
        [Column(TypeName = "varchar(-1)")]
        public string? notes { get; set; }
    }
}
