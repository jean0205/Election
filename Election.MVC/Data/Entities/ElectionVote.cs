using System.ComponentModel.DataAnnotations;

namespace Election.MVC.Data.Entities
{
    public class ElectionVote
    {
        public int Id { get; set; }

        public NationalElection? Election { get; set; }


        [Required(ErrorMessage = "{0} is a required field.")]
        public DateTime VoteTime { get; set; }

        public Voter? Voter { get; set; }

        public bool Voted { get; set; }

        public Party? SupportedParty { get; set; }

        public Comment? Comment { get; set; }

        public string? OtherComment { get; set; }

        public User? RecorderBy { get; set; }

        public bool Locked { get; set; }

        public User? LockedBy { get; set; }


    }
}
