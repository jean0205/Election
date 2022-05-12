using Election.MVC.Enums;
using System.ComponentModel.DataAnnotations;

namespace Election.MVC.Models
{
    public class AddUserViewModel : EditUserViewModel
    {
        [Display(Name = "Username")]       
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "{0} is a required field.")]
        public string Username { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email not valid.")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "{0} is a required field.")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "{0} is a required field.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "El campo {0} debe tener entre {2} y {1} carácteres.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and the comfirmation do not match.")]
        [Display(Name = "Confirmation")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "{0} is a required field.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "El campo {0} debe tener entre {2} y {1} carácteres.")]
        public string PasswordConfirm { get; set; }

        [Display(Name = "Tipo de usuario")]
        public UserType UserType { get; set; }
    }
}
