namespace Constituency.Desktop.Entities
{
    public class Voter
    {
        public int Id { get; set; }
        public int Reg { get; set; }

        public string SurName { get; set; }

        public string GivenNames { get; set; }

        public string Sex { get; set; }
        public DateTime DOB { get; set; }

        public string? Address { get; set; }

        public string? Occupation { get; set; }

        public string? Mobile1 { get; set; }

        public string? Mobile2 { get; set; }

        public string? HomePhone { get; set; }

        public string? WorkPhone { get; set; }

        public string? Email { get; set; }

        public PollingDivision? PollingDivision { get; set; }
        public House? House { get; set; }

        public ICollection<Interview>? Interviews { get; set; }

    }
}
