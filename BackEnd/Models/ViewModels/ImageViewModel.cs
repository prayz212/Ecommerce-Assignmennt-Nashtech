namespace BackEnd.Models.ViewModels
{
    public class ImageDto
    {
        public string Name { get; set; }
        public string Uri { get; set; }
        public string Format { get; set; }
        public long Width { get; set; }
        public long Height { get; set; }
        public long Size { get; set; }
    }
}