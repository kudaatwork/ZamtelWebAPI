using System;
using ZamtelWebAPI.Models;

namespace ZamtelWebAPI.ViewModels
{
    public class BackOfficeAgentViewModel
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public string MobileNumber { get; set; }
        public string AlternativeMobileNumber { get; set; }
        public int? SupervisorId { get; set; }
        public string Gender { get; set; }
        public string Area { get; set; }
        public int? TownId { get; set; }
        public int? ProvinceId { get; set; }
        public int? NationalityId { get; set; }
        public string NationalIdNumber { get; set; }
        public int? NextOfKinId { get; set; }
        public string PotrailUrl { get; set; }
        public string NationalIdFrontUrl { get; set; }
        public string NationalIdBackUrl { get; set; }
        public string SignatureUrl { get; set; }
        public string AgentContractFormUrl { get; set; }
        public bool? IsVerified { get; set; }
        public DateTime? DateTimeCreated { get; set; }
        public DateTime? DateTimeModified { get; set; }
        public int? CreatedByUserId { get; set; }
        public int? ModifiedByUserId { get; set; }
        public string Password { get; set; }
        public NextOfKin NextOfKin { get; set; }       
        public int? RoleId { get; set; }
    }
}
