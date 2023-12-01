using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMS_API.Contexts.Views
{
    public class vw_companies
    {
        [Key]
        public int id_company { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string company_name { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string? phone { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string? email { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string? contact { get; set; }
        [Column(TypeName = "varchar(-1)")]
        public string? url { get; set; }
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
    }
}
