using System.ComponentModel.DataAnnotations;

namespace Constituency.Desktop.Models
{
    public class AddUserViewModel
    {


        [Required]
        public bool Active { get; set; }
        [Required(ErrorMessage = "{0} is a required field.")]
        [Display(Name = "UserName")]
        public string Username { get; set; }

        [Required(ErrorMessage = "{0} is a required field.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "{0} is a required field.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "{0} is a required field.")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }


        [Display(Name = "phoneNumber")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Picture")]
        public Guid ImageId { get; set; }
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://loansgdbapi2.azurewebsites.net/images/noimage.png"
            : $"https://sbdfstorage.blob.core.windows.net/usersimages/{ImageId}";

        public byte[] ImageFile { get; set; }

    }
}
