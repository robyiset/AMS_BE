using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMS_API.Contexts.Views
{
    public class vw_user_details
    {
        [Key]
        public int id_user { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string username { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string email { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string? first_name { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string? last_name { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string? phone_number { get; set; }
        [Column(TypeName = "varchar(-1)")]
        public string? about { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string? company_name { get; set; }
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
    }
}
