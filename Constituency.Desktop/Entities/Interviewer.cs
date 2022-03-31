﻿namespace Constituency.Desktop.Entities
{
    public class Interviewer
    {
        public int Id { get; set; }
        public string SurName { get; set; }
        public string GivenNames { get; set; }
        public string? Sex { get; set; }
        public DateTime? DOB { get; set; }
        public string? Address { get; set; }
        public string? Occupation { get; set; }
        public string? Mobile1 { get; set; }
        public string? Mobile2 { get; set; }
        public string? HomePhone { get; set; }
        public string? WorkPhone { get; set; }
        public string? Email { get; set; }
        public ICollection<Interview>? Interviews { get; set; }
    }
}