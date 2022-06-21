using Microsoft.AspNetCore.Http;

namespace ZamtelWebAPI.Models
{
    public class UploadFile
    {       
        public IFormFile Signature { get; set; }
        public IFormFile IdFront { get; set; }
        public IFormFile IdBack { get; set; }
        public IFormFile Portrait { get; set; }
        public IFormFile ProofOfStay { get; set; }

        public string Name { get; set; }
    }
}
