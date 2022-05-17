using System.ComponentModel.DataAnnotations;

namespace Election.MVC.Models
{
    public class LoginViewModel
    {
        [Display(Name = "UserName")]
        [Required(ErrorMessage = "{0} is a required field.")]
        //[EmailAddress(ErrorMessage = "Invalid Email")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "{0} is a required field.")]
        [MinLength(6, ErrorMessage = "The Field  {0} must have at least {1} characters.")]
        public string Password { get; set; }

        [Display(Name = "Rememberme")]
        public bool RememberMe { get; set; }
    }
}
