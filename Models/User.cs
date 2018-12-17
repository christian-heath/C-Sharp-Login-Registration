using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace loginreg.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }


        [Required]
        [Display(Name = "First Name:")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First Name must contain letters only")]
        [MinLength(2, ErrorMessage = "First Name must be 2 characters or longer!")]
        public string FirstName { get; set; }


        [Required]
        [Display(Name = "Last Name:")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last Name must contain letters only")]
        [MinLength(2, ErrorMessage = "Last Name must be 2 characters or longer!")]
        public string LastName { get; set; }


        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address!")]
        [Display(Name = "Email Address:")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password:")]
        [MinLength(8, ErrorMessage = "Password must be 8 characters or longer!")]
        public string Password { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;


        // Will not be mapped to your users table!
        [NotMapped]
        [Display(Name = "Password Confirm:")]
        [Compare("Password", ErrorMessage = "Passwords do not match!")]
        [DataType(DataType.Password)]
        public string Confirm { get; set; }
    }
}