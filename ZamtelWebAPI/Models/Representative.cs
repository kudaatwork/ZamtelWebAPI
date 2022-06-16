using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ZamtelWebAPI.Models
{
    public partial class Representative
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public string IdType { get; set; }
        public string NationalIdNumber { get; set; }
        public string InvitingEntity { get; set; }
        public string PortraitUrl { get; set; }
        public string NationalIdFrontUrl { get; set; }
        public string NationalIdBackUrl { get; set; }
        public string SignatureUrl { get; set; }
        public DateTime? DateTimeCreated { get; set; }
        public DateTime? DateTimeModified { get; set; }
        public int? CreatedByUserId { get; set; }
        public int? ModifiedByUserId { get; set; }
    }
}
