using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMS_API.Contexts.Tables
{
    public class tbl_asset_maintenances
    {
        [Key]
        public int id_maintenance { get; set; }
        public int id_asset { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string title { get; set; }
        [Column(TypeName = "varchar(-1)")]
        public string maintenance_desc { get; set; }
        public int? id_warranty { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? completion_date { get; set; }
        public int? maintenance_time { get; set; }
        [Column(TypeName = "decimal(18,0)")]
        public decimal? cost { get; set; }
        [Column(TypeName = "varchar(-1)")]
        public string notes { get; set; }
        public DateTime? created_at { get; set; }
        public int? created_by { get; set; }
        public DateTime? updated_at { get; set; }
        public int? updated_by { get; set; }
        public DateTime? deleted_at { get; set; }
        public int? deleted_by { get; set; }
        public bool? deleted { get; set; }
    }
}
