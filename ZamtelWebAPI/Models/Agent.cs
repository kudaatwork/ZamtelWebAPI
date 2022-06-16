using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ZamtelWebAPI.Models
{
    public partial class Agent
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public string DeviceOwnership { get; set; }
        public int? SupervisorId { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string Area { get; set; }
        public int? TownId { get; set; }
        public int? ProvinceId { get; set; }
        public int? NationalityId { get; set; }
        public string IdNumber { get; set; }
        public string MobileNumber { get; set; }
        public string AlternativeMobileNumber { get; set; }
        public string AgentCode { get; set; }
        public string PotrailUrl { get; set; }
        public string NationalIdFrontUrl { get; set; }
        public string NationalIdBackUrl { get; set; }
        public string SignatureUrl { get; set; }
        public string AgentContractFormUrl { get; set; }
        public bool? IsVerified { get; set; }
        public int? NextOfKinId { get; set; }
        public DateTime? DateTimeCreated { get; set; }
        public DateTime? DateTimeModified { get; set; }
        public int? CreatedByUserId { get; set; }
        public int? ModifiedByUserId { get; set; }
    }
}
