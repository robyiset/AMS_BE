using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMS_API.Models
{
    public class suppliers
    {
        public int id_supplier { get; set; }
        [Required(ErrorMessage = "Supplier name is required")]
        public string supplier_name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string contact { get; set; }
        public string url { get; set; }
        public int? id_location { get; set; }
        public int? id_company { get; set; }
        public int id_user { get; set; }
    }
    public class supplier_company
    {
        [Required(ErrorMessage = "id is required")]
        public int id_supplier { get; set; }
        public SelectOrAddCompany company { get; set; }
    }
    public class supplier_location
    {
        [Required(ErrorMessage = "Id is required")]
        public int id_supplier { get; set; }
        public add_location location { get; set; }
    }
}
