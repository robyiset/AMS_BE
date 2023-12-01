using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMS_API.Contexts.Tables
{
    public class tbl_asset_logs
    {
        [Key]
        public int id_asset_log { get; set; }
        public int id_asset { get; set; }
        [Column(TypeName = "varchar(-1)")]
        public string action_desc { get; set; }
        public DateTime? log_date { get; set; }
    }
}
