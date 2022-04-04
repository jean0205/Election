namespace Constituency.Desktop.Entities
{
    public class Canvas
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public bool Open { get; set; }
        public CanvasType Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Interview>? Interviews { get; set; }

    }
}
