using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMS_API.Contexts.Tables
{
    public class tbl_asset_types
    {
        [Key]
        public int id_type { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string name_type { get; set; }
        [Column(TypeName = "varchar(-1)")]
        public string desc_type { get; set; }
        public DateTime? created_at { get; set; }
        public int? created_by { get; set; }
        public DateTime? updated_at { get; set; }
        public int? updated_by { get; set; }
        public DateTime? deleted_at { get; set; }
        public int? deleted_by { get; set; }
        public bool? deleted { get; set; }
    }
}
