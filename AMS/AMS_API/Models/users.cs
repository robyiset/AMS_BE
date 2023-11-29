﻿using System.ComponentModel.DataAnnotations;

namespace AMS_API.Models
{
    public class users
    {
    }
    public class register
    {
        [Required(ErrorMessage = "First name is required")]
        public string first_name { get; set; }

        public string last_name { get; set; }

        [Required(ErrorMessage = "Username is required")]
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
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string repeat_password { get; set; }
    }
}