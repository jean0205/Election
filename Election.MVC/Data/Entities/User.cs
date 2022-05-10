using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Election.MVC.Data.Entities
{
    public class User : IdentityUser
    {
        public static object Claims { get; internal set; }
        [Required]
        public bool Active { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        [NotMapped]
        public virtual IList<string>? Access { get; set; }

        [NotMapped]
        public byte[]? ImageFile { get; set; }

        [Display(Name = "Picture")]
        public Guid ImageId { get; set; }
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://loansgdbapi2.azurewebsites.net/images/noimage.jpg"
            : $"https://sbdfstorage.blob.core.windows.net/usersimages/{ImageId}";

        public bool Online { get; set; }
        public DateTime LogInTime { get; set; }
        public DateTime LogOutTime { get; set; }
    }
}
