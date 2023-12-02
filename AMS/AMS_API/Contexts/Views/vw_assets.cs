using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMS_API.Contexts.Views
{
    public class vw_assets
    {   
        [Key]
        public int id_asset { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string serial { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string asset_name { get; set; }
        [Column(TypeName = "varchar(-1)")]
        public string asset_desc { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string name_type { get; set; }
        [Column(TypeName = "varchar(201)")]
        public string assigned_user { get; set; }
        public DateTime? purchase_date { get; set; }
        [Column(TypeName = "decimal(18,0)")]
        public decimal? purchase_cost { get; set; }
        public bool depreciate { get; set; }
        public bool requestable { get; set; }
        public bool consumable { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string company_name { get; set; }
        public DateTime? warranty_expiration { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string status { get; set; }
        [Column(TypeName = "varchar(-1)")]
        public string address { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string city { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string state { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string country { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string zip { get; set; }
        [Column(TypeName = "varchar(-1)")]
        public string details { get; set; }
    }
}
