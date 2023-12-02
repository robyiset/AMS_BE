using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMS_API.Contexts.Views
{
    public class vw_consumable_assets
    {
        [Key]
        public int? id_usage { get; set; }
        public int id_asset { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string asset_name { get; set; }
        [Column(TypeName = "varchar(201)")]
        public string? consumed_by_user { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string? consumed_by_company { get; set; }
        public DateTime? purchase_date { get; set; }
        [Column(TypeName = "decimal(18,0)")]
        public decimal? purchase_cost { get; set; }
    }
}
