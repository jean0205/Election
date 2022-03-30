using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Constituency.Desktop.Models
{
    public class ChangePasswordViewModel
    {
        [Display(Name = "Old Password")]
        [Required(ErrorMessage = "The Field {0} is required.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "The Field {0} must have at least {1} characters.")]
        public string OldPassword { get; set; }

        [Display(Name = "New Password")]
        [Required(ErrorMessage = "The Field {0} is required.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "The Field {0} must have at least {1} characters.")]
        public string NewPassword { get; set; }

        [Display(Name = "New Password")]
        [Required(ErrorMessage = "The Field {0} is required.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "The Field {0} must have at least {1} characters.")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation are not the same.")]
        public string Confirm { get; set; }
    }
}
