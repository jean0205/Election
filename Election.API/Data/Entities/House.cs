using System.ComponentModel.DataAnnotations;

namespace Election.API.Data.Entities
{
    public class House
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is a required field.")]
        public bool Active { get; set; }
        public string? Number { get; set; }
        public int NumberOfPersons { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public ICollection<Voter>? Voters { get; set; }
    }
}
