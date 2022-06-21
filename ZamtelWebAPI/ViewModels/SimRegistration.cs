using Microsoft.AspNetCore.Http;

namespace ZamtelWebAPI.ViewModels
{
    public class SimRegistration
    {
        public IFormFile Signature { get; set; }
        public IFormFile NationalIdFront { get; set; }
        public IFormFile NationalIdBack { get; set; }
        public IFormFile Portrait { get; set; }
        public IFormFile ProofOfStay { get; set; }
        public IFormFile LastDayOfStaySupportingDocument { get; set; }
        public string SignatureBase64 { get; set; }
        public string SimRegistrationDetails { get; set; }
    }
}
