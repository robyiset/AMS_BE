using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMS_API.Contexts.Tables
{
    public class tbl_requested_assets
    {
        [Key]
        public int id_request { get; set; }
        public int id_asset { get; set; }
        public int? id_user { get; set; }
        public int? id_company { get; set; }
        public int? requested_at { get; set; }
        public int? denied_at { get; set; }
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
