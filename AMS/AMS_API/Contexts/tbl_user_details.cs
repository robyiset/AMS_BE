using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMS_API.Contexts
{
    [Table("tbl_user_details")]
    public class tbl_user_details
    {
        [Key]
        public int id_user_detail { get; set; }
        public int id_user { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string phone_number { get; set; }
        [Column(TypeName = "varchar(-1)")]
        public string address { get; set; }
        public int? id_location { get; set; }
        public int? id_company { get; set; }
        public DateTime? created_at { get; set; }
        public int? created_by { get; set; }
        public DateTime? updated_at { get; set; }
        public int? updated_by { get; set; }
        public DateTime? deleted_at { get; set; }
        public int? deleted_by { get; set; }
        public bool? deleted { get; set; }
    }
}
