using System.ComponentModel.DataAnnotations;

namespace Constituency.Desktop.Models
{
    public class ResetPasswordViewModel
    {
       public string UserName { get; set; }        
        public string Password { get; set; }        
        public string ConfirmPassword { get; set; }       
        public string Token { get; set; }
    }
}
