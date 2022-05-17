using System.ComponentModel.DataAnnotations;

namespace Election.MVC.Data.Entities
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

        [Display(Name = "Canvas Description")]
        [MaxLength(1000)]
        public string? Description { get; set; }

        public ICollection<Canvas>? Canvas { get; set; }
    }
}
