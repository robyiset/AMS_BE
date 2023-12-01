using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMS_API.Contexts.Tables
{
    public class tbl_asset_waranties
    {
        [Key]
        public int id_warranty { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string warranty_name { get; set; }
        public DateTime? warranty_expiration { get; set; }
        public DateTime? created_at { get; set; }
        public int? created_by { get; set; }
        public DateTime? updated_at { get; set; }
        public int? updated_by { get; set; }
        public DateTime? deleted_at { get; set; }
        public int? deleted_by { get; set; }
        public bool? deleted { get; set; }
    }
}
