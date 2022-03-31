using System.ComponentModel.DataAnnotations;

namespace Constituency.Desktop.Entities
{
    public class PollingDivision
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is a required field.")]
        public bool Active { get; set; }
        public string Name { get; set; }
        public ConstituencyC? Constituency { get; set; }
    }
}
