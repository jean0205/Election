using System.ComponentModel.DataAnnotations;

namespace Election.API.Data.Entities
{
    public class ElectionVote
    {
        public int Id { get; set; }
        

        [Required(ErrorMessage = "{0} is a required field.")]
        public DateTime VoteTime { get; set; }


        [Required(ErrorMessage = "{0} is a required field.")]
        public Voter Voter { get; set; }

        public bool Voted { get; set; }

        public Party? SupportedParty { get; set; }

        public Comment? Comment { get; set; }
        
        public string? OtherComment { get; set; }
        
        public Interviewer? Interviewer { get; set; }
    }
}
