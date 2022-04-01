using System.ComponentModel.DataAnnotations;

namespace Election.API.Data.Entities
{
    public class CanvasType
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is a required field.")]
        public bool Active { get; set; }

        [Required(ErrorMessage = "{0} is a required field.")]
        [Display(Name = "Canvas Type")]
        [MaxLength(100)]
        public string Type { get; set; }
    }
}
