using Microsoft.AspNetCore.Http;

namespace ZamtelWebAPI.Models
{
    public class UploadFile
    {
        public int Id { get; set; }
        public IFormFile Files { get; set; }
        public string Name { get; set; }
    }
}
