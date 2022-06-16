using System;
using ZamtelWebAPI.Models;

namespace ZamtelWebAPI.ViewModels
{
    public class SimRegistrationViewModel
    {
        public int Id { get; set; }
        public string RegistrationType { get; set; }
        public string CategoryType { get; set; }        
        public string SimSerialNumber { get; set; }
        public int? CustomerId { get; set; }
        public int? CorporateId { get; set; }
        public DateTime? DateTimeCreated { get; set; }
        public DateTime? DateTimeModified { get; set; }
        public int? CreatedByUserId { get; set; }
        public int? ModifiedByUserId { get; set; }
        public CustomerViewModel Customer { get; set; }
        public CorporateViewModel Corporate { get; set; }
    }
}
