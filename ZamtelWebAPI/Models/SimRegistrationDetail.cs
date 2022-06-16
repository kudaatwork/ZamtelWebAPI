using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ZamtelWebAPI.Models
{
    public partial class SimRegistrationDetail
    {
        public int Id { get; set; }
        public string RegistrationType { get; set; }
        public string CategoryType { get; set; }
        public string SimSerialNumber { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? DateTimeCreated { get; set; }
        public DateTime? DateTimeModified { get; set; }
        public int? CreatedByUserId { get; set; }
        public int? ModifiedByUserId { get; set; }
        public int? CorporateId { get; set; }
    }
}
