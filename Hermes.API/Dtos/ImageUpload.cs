namespace Hermes.API.Dtos
{
    public class ImageUpload
    {
        public IFormFile? File { get; set; }
        public List<IFormFile>? FilesList { get; set; }
    }
}
