using FluentValidation;
using ZamtelWebAPI.Models;
using System.Linq;
using ZamtelWebAPI.ViewModels;

namespace ZamtelWebAPI.Validators
{
    public class BackOfficeAgentValidator : AbstractValidator<BackOfficeAgentViewModel>
    {
        public BackOfficeAgentValidator()
        {
            RuleFor(x => x.Firstname).NotEmpty().NotNull();

            RuleFor(x => x.Lastname).NotEmpty().NotNull();

            RuleFor(x => x.MobileNumber).NotEmpty().NotNull().Matches(@"^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$").NotEqual(x => x.AlternativeMobileNumber);

            RuleFor(x => x.AlternativeMobileNumber).NotEmpty().NotNull().Matches(@"^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$").NotEqual(x => x.MobileNumber);

            RuleFor(x => x.SupervisorId).NotEmpty().NotNull();

            RuleFor(x => x.Area).NotEmpty().NotNull();

            RuleFor(x => x.TownId).NotEmpty().NotNull();

            RuleFor(x => x.ProvinceId).NotEmpty().NotNull();

            RuleFor(x => x.NationalityId).NotEmpty().NotNull();

            RuleFor(x => x.RoleId).NotEmpty().NotNull();

            RuleFor(x => x.NationalIdNumber).NotEmpty().NotNull();

            RuleFor(x => x.NextOfKin).NotNull();

            RuleFor(x => x.NextOfKin.Firstname).NotEmpty().NotNull();

            RuleFor(x => x.NextOfKin.Lastname).NotEmpty().NotNull();

            RuleFor(x => x.NextOfKin.MobileNumber).NotEmpty().NotNull();
        }
    }

    public class BackOfficeAgentValidators
    {
        private readonly ZamtelContext _context;

        public BackOfficeAgentValidators(ZamtelContext context)
        {
            _context = context;
        }

        public bool IsBackOfficeAgentMobileNumberExist(string mobileNumber)
        {
            var mobileNumberExists = _context.BackOfficeAgents.Where(x => x.MobileNumber == mobileNumber).FirstOrDefault();

            return mobileNumberExists != null;
        }

        public bool IsNextOfKinMobileNumberExist(string mobileNumber)
        {
            var mobileNumberExists = _context.NextOfKins.Where(x => x.MobileNumber == mobileNumber).FirstOrDefault();

            return mobileNumberExists != null;
        }

        public bool IsBackOfficeAgentIdNumberExist(string nationalIdNumber)
        {
            var nationalIdNumberExists = _context.BackOfficeAgents.Where(x => x.NationalIdNumber == nationalIdNumber).FirstOrDefault();

            return nationalIdNumberExists != null;
        }
    }
}
