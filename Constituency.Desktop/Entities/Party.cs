using System.ComponentModel.DataAnnotations;

namespace Constituency.Desktop.Entities
{
    public class Party
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
    }
}
