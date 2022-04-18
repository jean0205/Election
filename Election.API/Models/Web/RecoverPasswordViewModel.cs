using System.ComponentModel.DataAnnotations;

namespace Election.API.Models.Web

{
    public class RecoverPasswordViewModel
    {
        [Required(ErrorMessage = "The Field {0} is required.")]
        public string userName { get; set; }
    }
}
