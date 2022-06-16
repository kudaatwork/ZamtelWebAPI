using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ZamtelWebAPI.Models
{
    public partial class AuditLog
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? AgentId { get; set; }
        public string Action { get; set; }
        public string Status { get; set; }
        public string RequestObject { get; set; }
        public string ResponseObject { get; set; }
        public string IpAddress { get; set; }
        public DateTime? DateTimeLogged { get; set; }
    }
}
