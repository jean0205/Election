using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constituency.Desktop.Entities
{
    public class ElectionVote
    {
        public int Id { get; set; }
       
        public DateTime VoteTime { get; set; }
      
        public Voter Voter { get; set; }
       
        public Party SupportedParty { get; set; }

        public Comment? Comment { get; set; }

        public string? OtherComment { get; set; }

        public Interviewer? Interviewer { get; set; }
    }
}
