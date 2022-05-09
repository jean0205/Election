using System.ComponentModel.DataAnnotations;

namespace Election.API.Models
{
    public class EditUserViewModel
    {
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string PhoneNumber { get; set; }
    }
}
