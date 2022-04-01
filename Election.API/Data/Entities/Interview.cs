using System.ComponentModel.DataAnnotations;

namespace Election.API.Data.Entities
{
    public class Interview
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is a required field.")]
        public bool Active { get; set; }

        [Required(ErrorMessage = "{0} is a required field.")]       
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "{0} is a required field.")]
        public Voter Voter { get; set; }

        [Required(ErrorMessage = "{0} is a required field.")]
        public Party SupportedParty { get; set; }    

        public Comment? Comment { get; set; }

        public string? OtherComment { get; set; }
       
        public Interviewer? Interviewer { get; set; }

    }
}
