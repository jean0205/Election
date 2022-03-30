using System.ComponentModel.DataAnnotations;

namespace Constituency.Desktop.Models

{
    public class RecoverPasswordViewModel
    {
        [Required(ErrorMessage = "The Field {0} is required.")]
        public string userName { get; set; }
    }
}
