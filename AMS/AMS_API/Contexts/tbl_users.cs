using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMS_API.Contexts
{
    [Table("tbl_users")]
    public class tbl_users
    {
        [Key]
        public int id_user { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string first_name { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string last_name { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string username { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string email { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string password { get; set; }
        public DateTime? last_login { get; set; }
        public DateTime? created_at { get; set; }
        public int? created_by { get; set; }
        public DateTime? updated_at { get; set; }
        public int? updated_by { get; set; }
        public DateTime? deleted_at { get; set; }
        public int? deleted_by { get; set; }
        public bool? deleted { get; set; }
    }
}
