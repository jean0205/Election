using System.ComponentModel.DataAnnotations;

namespace Election.API.Data.Entities
{
    public class Canvas
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is a required field.")]
        public bool Active { get; set; }
        
        [Required(ErrorMessage = "{0} is a required field.")]
        public bool Open { get; set; }

        [Required(ErrorMessage = "{0} is a required field.")]
        public CanvasType Type { get; set; }

        [Required(ErrorMessage = "{0} is a required field.")]
        [Display(Name = "Name")]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [Display(Name = "Description")]
        [MaxLength(1000)]
        public string Description { get; set; }   

       public ICollection<Interview>? Interviews { get; set; }

    }
}
