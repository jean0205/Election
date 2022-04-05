using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constituency.Desktop.Entities
{
    internal class NationalElection
    {
        public int Id { get; set; }

        public bool Open { get; set; }
       
        public DateTime ElectionDate { get; set; }

        public string? Description { get; set; }

        public ICollection<ElectionVote>? ElectionVotes { get; set; }

        public ICollection<Party>? Parties { get; set; }
    }
}
