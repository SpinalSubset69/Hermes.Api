namespace Hermes.API.Dtos
{
    public class ImageUpload
    {
        public InputImageRequest? Input { get; set; }
        public List<IFormFile>? FilesList { get; set; }
    }
}
