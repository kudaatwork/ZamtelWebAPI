using System;
using ZamtelWebAPI.Models;

namespace ZamtelWebAPI.ViewModels
{
    public class CorporateViewModel
    {
        public int Id { get; set; }       
        public string CorporateIdNumber { get; set; }
        public string CertificateUrl { get; set; }
        public string LetterUrl { get; set; }
        public string BatchUrl { get; set; }
        public int? RepresantativeId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCoinumber { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public int? ProvinceId { get; set; }
        public int? TownId { get; set; }
        public string Address { get; set; }
        public string CompanyEmail { get; set; }
        public string AlternativeMobileNumber { get; set; }
        public string CorporateMobileNumber { get; set; }
        public DateTime? DateTimeCreated { get; set; }
        public DateTime? DateTimeModified { get; set; }
        public int? CreatedByUserId { get; set; }
        public int? ModifiedByUserId { get; set; }
        public bool? IsCompanyGo { get; set; }
        public Representative Representative { get; set; }
    }
}
