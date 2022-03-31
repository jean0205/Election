namespace Constituency.Desktop.Entities
{
    public class Canvas
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CanvasType Type { get; set; }
        public ICollection<Interview>? Interviews { get; set; }

    }
}
