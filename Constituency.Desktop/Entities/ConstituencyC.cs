
using System.ComponentModel.DataAnnotations;

namespace Constituency.Desktop.Entities
{
    public class ConstituencyC
    {
        public int Id { get; set; } 
       public bool Active { get; set; }
        public string SGSE { get; set; }       
        public string Name { get; set; }
        public ICollection<PollingDivision>? PollingDivisions { get; set; }
    }
}
