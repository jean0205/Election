using System.ComponentModel.DataAnnotations;

namespace Election.MVC.Data.Entities
{
    public class PollingDivision
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is a required field.")]
        public bool Active { get; set; }

        [Required(ErrorMessage = "{0} is a required field.")]
        [MaxLength(50)]
        public string Name { get; set; }

        public Constituency? Constituency { get; set; }
    }
}
