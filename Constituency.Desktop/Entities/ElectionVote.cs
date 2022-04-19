namespace Constituency.Desktop.Entities
{
    public class ElectionVote
    {
        public int Id { get; set; }

        public NationalElection Election { get; set; }
        public DateTime VoteTime { get; set; }

        public Voter Voter { get; set; }

        public Party? SupportedParty { get; set; }

        public Comment? Comment { get; set; }

        public string? OtherComment { get; set; }

        public User? RecorderBy { get; set; }       

        public bool Locked { get; set; }

        public User? LockedBy { get; set; }

    }
}
