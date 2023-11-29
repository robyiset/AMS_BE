using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMS_API.Contexts
{
    public class tbl_licences
    {
        [Key]
        public int id_license { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string license_name { get; set; }
        [Column(TypeName = "varchar(-1)")]
        public string license_desc { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string license_account { get; set; }
        public int? id_user { get; set; }
        public int? id_asset { get; set; }
        public DateTime? purchase_date { get; set; }
        [Column(TypeName = "decimal(18,0)")]
        public decimal? purchase_cost { get; set; }
        public DateTime? expired_date { get; set; }
        public DateTime? termination_date { get; set; }
        public int? id_warranty { get; set; }
        public DateTime? created_at { get; set; }
        public int? created_by { get; set; }
        public DateTime? updated_at { get; set; }
        public int? updated_by { get; set; }
        public DateTime? deleted_at { get; set; }
        public int? deleted_by { get; set; }
        public bool? deleted { get; set; }
    }
}
