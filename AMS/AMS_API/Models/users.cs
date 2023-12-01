using System.ComponentModel.DataAnnotations;

namespace AMS_API.Models
{
    public class users
    {
    }
    public class register
    {

        [Required(ErrorMessage = "Username is required")]
        [RegularExpression(@"^[a-zA-Z0-9_]{3,20}$", ErrorMessage = "Username must be 3 to 20 characters long and contain only letters, numbers, and underscores")]
        public string username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$",
            ErrorMessage = "Password must be at least 8 characters and contain at least one lowercase letter, one uppercase letter, and one digit")]
        public string password { get; set; }

        [Required(ErrorMessage = "Repeat password is required")]
        [Compare("password", ErrorMessage = "Passwords do not match")]
        public string repeat_password { get; set; }
    }
    public class profile
    {
        public int id_user { get; set; }
        [Required(ErrorMessage = "First name is required")]
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string about { get; set; }
        [RegularExpression(@"^\+?[0-9]{10,15}$", ErrorMessage = "Invalid phone number")]
        public string phone_number { get; set; }
    }
    public class user_location
    {
        [Required(ErrorMessage = "id is required")]
        public int id_user { get; set; }
        public locations location { get; set; }

    }
    public class user_company
    {
        [Required(ErrorMessage = "id is required")]
        public int id_user { get; set; }
        public SelectOrAddCompany company { get; set; }
    }
    public class user_details
    {
        public profile user_profile { get; set; }
        public locations user_location { get; set; }
        public companies user_company { get; set; }
    }
}
