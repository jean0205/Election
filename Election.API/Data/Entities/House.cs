namespace Election.API.Data.Entities
{
    public class House
    {
        public int Id { get; set; }

        public ICollection<Voter>? Voters { get; set; }
    }
}
