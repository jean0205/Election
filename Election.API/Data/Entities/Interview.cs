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

        
        public Voter? Voter { get; set; }

       
        public Canvas? Canvas { get; set; }

       
        public Party? SupportedParty { get; set; }    

        public Comment? Comment { get; set; }

        public string? OtherComment { get; set; }
       
        public Interviewer? Interviewer { get; set; }
        
        public bool Locked { get; set; }
        
        public User? LockedBy { get; set; }

        public User? RecorderBy { get; set; }

    }
}
