using System.ComponentModel.DataAnnotations;

namespace Election.API.Data.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is a required field.")]
        [Display(Name = "Comment")]
        [MaxLength(2000)]
        public string Text { get; set; }
    }
}
