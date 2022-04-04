namespace Constituency.Desktop.Entities
{
    public class CanvasType
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public string Type { get; set; }
        public string? Description { get; set; }
        public ICollection<Canvas>? Canvas { get; set; }
    }
}
