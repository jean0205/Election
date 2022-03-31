namespace Constituency.Desktop.Entities
{
    public class Interview
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public Voter Voter { get; set; }

        public Party SupportedParty { get; set; }

        public Comment? Comment { get; set; }

        public string? OtherComment { get; set; }

        public Interviewer? Interviewer { get; set; }

    }
}
