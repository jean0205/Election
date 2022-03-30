using System.ComponentModel.DataAnnotations;

namespace Election.API.Data.Entities
{
    public class Voter
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is a required field.")]
        [Display(Name = "Registration Number")]
        [MaxLength(20)]
        public int Reg { get; set; }


        [Required(ErrorMessage = "{0} is a required field.")]
        [Display(Name = "Surname")]
        [MaxLength(50)]
        public string SurName { get; set; }

        [Required(ErrorMessage = "{0} is a required field.")]
        [Display(Name = "Given Names")]
        [MaxLength(100)]
        public string GivenNames { get; set; }

        [Required(ErrorMessage = "{0} is a required field.")]
        [MaxLength(10)]
        public string Sex { get; set; }


        [Required(ErrorMessage = "{0} is a required field.")]
        [Display(Name = "Date of Birth")]        
        public DateTime DOB { get; set; }
        
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
        [EmailAddress]
        public string? Email { get; set; }

        public PollingDivision? PollingDivision { get; set; }
        public House? House { get; set; }

        public ICollection<Interview>? Interviews { get; set; }

    }
}
