using System;
using ZamtelWebAPI.Models;

namespace ZamtelWebAPI.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public int? NationalityId { get; set; }
        public string IdType { get; set; }
        public string NationalIdNumber { get; set; }
        public int? ProvinceId { get; set; }
        public int? TownId { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Occupation { get; set; }
        public string MobileNumber { get; set; }
        public string AlternativeMobileNumber { get; set; }
        public string PlotNumber { get; set; }
        public string UnitNumber { get; set; }
        public string Village { get; set; }
        public string Landmark { get; set; }
        public string Road { get; set; }
        public string Chiefdom { get; set; }
        public string Neighborhood { get; set; }
        public string Section { get; set; }
        public string NationalIdFrontUrl { get; set; }
        public string NationalIdBackUrl { get; set; }
        public string PortraitUrl { get; set; }
        public string SignatureUrl { get; set; }
        public bool? IsForeigner { get; set; }
        public DateTime? DateTimeCreated { get; set; }
        public DateTime? DateTimeModified { get; set; }
        public int? CreatedByUserId { get; set; }
        public int? ModifiedByUserId { get; set; }
        public string ProofOfStayUrl { get; set; }
        public bool? IsMinor { get; set; }
        public int? NextOfKinId { get; set; }
        public DateTime? LastDayOfStay { get; set; }
        public string LastDayOfStaySupportingDocumentUrl { get; set; }
        public NextOfKin NextOfKin { get; set; }
    }
}
