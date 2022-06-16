using FluentValidation;
using ZamtelWebAPI.Models;
using System.Linq;
using ZamtelWebAPI.ViewModels;

namespace ZamtelWebAPI.Validators
{
    public class AgentValidator : AbstractValidator<AgentViewModel>
    {
        public AgentValidator()
        {
            RuleFor(x => x.Firstname).NotEmpty().NotNull();

            RuleFor(x => x.Lastname).NotEmpty().NotNull();

            RuleFor(x => x.DeviceOwnership).NotEmpty().NotNull();

            RuleFor(x => x.SupervisorId).NotEmpty().NotNull();

            RuleFor(x => x.Password).NotEmpty().NotNull().NotNull();

            RuleFor(x => x.Gender).NotEmpty().NotNull();

            RuleFor(x => x.Area).NotEmpty().NotNull();

            RuleFor(x => x.TownId).NotEmpty().NotNull();

            RuleFor(x => x.ProvinceId).NotEmpty().NotNull();

            RuleFor(x => x.NationalityId).NotEmpty().NotNull();

            RuleFor(x => x.IdNumber).NotEmpty().NotNull();

            RuleFor(x => x.MobileNumber).NotEmpty().NotNull().Matches(@"^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$").NotEqual(x => x.AlternativeMobileNumber);

            RuleFor(x => x.AlternativeMobileNumber).NotEmpty().NotNull().Matches(@"^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$").NotEqual(x => x.MobileNumber);

            RuleFor(x => x.PotrailUrl).NotEmpty().NotNull();

            RuleFor(x => x.NationalIdFrontUrl).NotEmpty().NotNull();

            RuleFor(x => x.NationalIdBackUrl).NotEmpty().NotNull();

            RuleFor(x => x.SignatureUrl).NotEmpty().NotNull();

            RuleFor(x => x.NextOfKin).NotNull();

            RuleFor(x => x.NextOfKin.Firstname).NotEmpty().NotNull();

            RuleFor(x => x.NextOfKin.Lastname).NotEmpty().NotNull();

            RuleFor(x => x.NextOfKin.MobileNumber).NotEmpty().NotNull();
        }
    }

    public class AgentValidators
    {
        private readonly ZamtelContext _context;

        public AgentValidators(ZamtelContext context)
        {
            _context = context;
        }

        public bool IsAgentMobileNumberExist(string mobileNumber)
        {
            var mobileNumberExists = _context.Agents.Where(x => x.MobileNumber == mobileNumber).FirstOrDefault();

            return mobileNumberExists != null;
        }

        public bool IsNextOfKinMobileNumberExist(string mobileNumber)
        {
            var mobileNumberExists = _context.NextOfKins.Where(x => x.MobileNumber == mobileNumber).FirstOrDefault();

            return mobileNumberExists != null;
        }

        public bool IsAgentIdNumberExist(string nationalIdNumber)
        {
            var nationalIdNumberExists = _context.Agents.Where(x => x.IdNumber == nationalIdNumber).FirstOrDefault();

            return nationalIdNumberExists != null;
        }
    }
}
