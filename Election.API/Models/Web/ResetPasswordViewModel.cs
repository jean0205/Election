using System.ComponentModel.DataAnnotations;

namespace Election.API.Models.Web
{
    public class ResetPasswordViewModel
    {
        [Display(Name = "User-Name")]
        [Required(ErrorMessage = "The Field {0} is required.")]       
        public string UserName { get; set; }

        [Display(Name = "New Password")]
        [Required(ErrorMessage = "The Field {0} is required.")]
        [MinLength(6, ErrorMessage = "The Field {0} minimun lenght is  {1} carácteres.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "The Field {0} is required.")]
        [MinLength(6, ErrorMessage = "The Field {0} minimun lenght is  {1} carácteres.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = " The Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
