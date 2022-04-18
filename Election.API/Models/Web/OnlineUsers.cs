using System.ComponentModel.DataAnnotations;

namespace Election.API.Models.Web
{
    public class OnlineUsers
    {
        [Display(Name = "User-Name")]
        [Required(ErrorMessage = "The Field {0} is required.")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

      
    }
}
