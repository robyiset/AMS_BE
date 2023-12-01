using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMS_API.Contexts.Views
{
    public class vw_users
    {
        [Key]
        public int id_user { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string username { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string email { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string first_name { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string last_name { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string phone_number { get; set; }
        [Column(TypeName = "varchar(-1)")]
        public string about { get; set; }
    }
}
