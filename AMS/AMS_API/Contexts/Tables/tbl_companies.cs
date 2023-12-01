using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMS_API.Contexts.Tables
{
    [Table("tbl_companies")]
    public class tbl_companies
    {
        [Key]
        public int id_company { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string company_name { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string phone { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string email { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string contact { get; set; }
        [Column(TypeName = "varchar(-1)")]
        public string url { get; set; }
        public int? id_location { get; set; }
        public DateTime? created_at { get; set; }
        public int? created_by { get; set; }
        public DateTime? updated_at { get; set; }
        public int? updated_by { get; set; }
        public DateTime? deleted_at { get; set; }
        public int? deleted_by { get; set; }
        public bool? deleted { get; set; }
    }
}
