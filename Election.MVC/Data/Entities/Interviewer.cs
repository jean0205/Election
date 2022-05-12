using System.ComponentModel.DataAnnotations;

namespace Election.MVC.Data.Entities
{
    public class Interviewer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is a required field.")]
        public bool Active { get; set; }

        [Required(ErrorMessage = "{0} is a required field.")]
        [Display(Name = "Surname")]
        [MaxLength(50)]
        public string SurName { get; set; }

        [Required(ErrorMessage = "{0} is a required field.")]
        [Display(Name = "Given Names")]
        [MaxLength(100)]
        public string GivenNames { get; set; }


        public string FullName => $"{SurName} {GivenNames}";
        

        [MaxLength(10)]
        public string? Sex { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime? DOB { get; set; }


        [MaxLength(200)]
        public string? Address { get; set; }


        [MaxLength(100)]
        public string? Occupation { get; set; }


        [MaxLength(11)]
        public string? Mobile1 { get; set; }

        [MaxLength(11)]
        public string? Mobile2 { get; set; }

        [MaxLength(11)]
        public string? HomePhone { get; set; }

        [MaxLength(11)]
        public string? WorkPhone { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        public ICollection<Interview>? Interviews { get; set; }
        public ICollection<ElectionVote>? ElectionVotes { get; set; }
    }
}
