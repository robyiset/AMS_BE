using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMS_API.Contexts.Views
{
    public class vw_maintenance
    {
        [Key]
        public int? id_maintenance { get; set; }
        public int id_asset { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string asset_name { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string? title { get; set; }
        [Column(TypeName = "varchar(-1)")]
        public string? maintenance_desc { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? completion_date { get; set; }
        public int? maintenance_time { get; set; }
        [Column(TypeName = "decimal(18,0)")]
        public decimal? cost { get; set; }
        [Column(TypeName = "varchar(-1)")]
        public string? notes { get; set; }
        [Column(TypeName = "varchar(-1)")]
        public string? address { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string? city { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string? state { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string? country { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string? zip { get; set; }
        [Column(TypeName = "varchar(-1)")]
        public string? details { get; set; }
    }
}
