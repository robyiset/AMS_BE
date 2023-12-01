using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMS_API.Models
{
    public class companies
    {
        public int id_company { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string company_name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string contact { get; set; }
        public string url { get; set; }
        public int id_user { get; set; }
    }
    public class company_location
    {
        [Required(ErrorMessage = "Id is required")]
        public int id_company { get; set; }
        public locations location { get; set; }
    }
    public class SelectOrAddCompany
    {
        public int id_company { get; set; }
        public string company_name { get; set; }
    }
}
