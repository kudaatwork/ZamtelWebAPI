using Microsoft.AspNetCore.Http;

namespace ZamtelWebAPI.ViewModels
{
    public class AgentRegistration
    {
        public IFormFile Portrait { get; set; }
        public IFormFile NationalIdFront { get; set; }
        public IFormFile NationalIdBack { get; set; }
        public IFormFile Signature { get; set; }
        public IFormFile AgentContractForm { get; set; }
        public string SignatureBase64 { get; set; }
        public string AgentRegistrationDetails { get; set; }
    }
}
