using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMS_API.Contexts.Tables
{
    [Table("tbl_locations")]
    public class tbl_locations
    {
        [Key]
        public int id_location { get; set; }
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
        public int? id_user { get; set; }
        public int? id_company { get; set; }
        public int? id_supplier { get; set; }
        public int? id_asset { get; set; }
        public int? id_usage { get; set; }
        public int? id_maintenance { get; set; }
        public DateTime? created_at { get; set; }
        public int? created_by { get; set; }
        public DateTime? updated_at { get; set; }
        public int? updated_by { get; set; }
        public DateTime? deleted_at { get; set; }
        public int? deleted_by { get; set; }
        public bool? deleted { get; set; }
    }
}
