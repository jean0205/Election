using System.ComponentModel.DataAnnotations;

namespace Election.API.Data.Entities
{
    public class NationalElection
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "{0} is a required field.")]
        public bool Active { get; set; }


        [Required(ErrorMessage = "{0} is a required field.")]
        public DateTime ElectionDate { get; set; }

        public string? Description { get; set; }
        
        public ICollection<ElectionVote>? ElectionVotes { get; set; }

        public ICollection<Party>? Parties { get; set; }        
    }
}
