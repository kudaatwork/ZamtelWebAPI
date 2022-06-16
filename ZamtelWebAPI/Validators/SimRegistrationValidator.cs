using FluentValidation;
using System.Linq;
using ZamtelWebAPI.Models;
using ZamtelWebAPI.ViewModels;

namespace ZamtelWebAPI.Validators
{
    public class SimRegistrationValidator : AbstractValidator<SimRegistrationViewModel>
    {
        public SimRegistrationValidator()
        {
            RuleFor(x => x.RegistrationType).NotEmpty().NotNull();

            RuleFor(x => x.CategoryType).NotEmpty().NotNull();            

            RuleFor(x => x.SimSerialNumber).NotEmpty().NotNull();

            When(x => x.Customer == null, () => 
            { 
                RuleFor(x => x.Corporate).NotNull(); 
            }).Otherwise(() => 
            { 
                RuleFor(x => x.Customer).NotNull(); 
            });

            When(x => x.Customer != null, () =>
            {
                RuleFor(x => x.Customer.Firstname).NotEmpty().NotNull();

                RuleFor(x => x.Customer.Lastname).NotEmpty().NotNull();

                RuleFor(x => x.Customer.Gender).NotEmpty().NotNull();

                RuleFor(x => x.Customer.DateOfBirth).NotEmpty().NotNull();

                RuleFor(x => x.Customer.NationalityId).NotEmpty().NotNull();

                RuleFor(x => x.Customer.IdType).NotEmpty().NotNull();

                RuleFor(x => x.Customer.NationalIdNumber).NotEmpty().NotNull();

                RuleFor(x => x.Customer.TownId).NotEmpty().NotNull();

                RuleFor(x => x.Customer.Area).NotEmpty().NotNull();

                RuleFor(x => x.Customer.Address).NotEmpty().NotNull();

                RuleFor(x => x.Customer.Email).NotEmpty().NotNull();

                RuleFor(x => x.Customer.Occupation).NotEmpty().NotNull();

                RuleFor(x => x.Customer.MobileNumber).NotEmpty().NotNull().Matches(@"^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$").NotEqual(x => x.Customer.AlternativeMobileNumber);

                RuleFor(x => x.Customer.AlternativeMobileNumber).NotEmpty().NotNull().Matches(@"^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$").NotEqual(x => x.Customer.MobileNumber);
            });

            When(x => x.Corporate != null, () =>
            {
                RuleFor(x => x.Corporate.CorporateIdNumber).NotEmpty().NotNull();

                RuleFor(x => x.Corporate.CompanyName).NotEmpty().NotNull();

                RuleFor(x => x.Corporate.CompanyCoinumber).NotEmpty().NotNull();

                RuleFor(x => x.Corporate.RegistrationDate).NotEmpty().NotNull();

                RuleFor(x => x.Corporate.ProvinceId).GreaterThan(0).NotNull();

                RuleFor(x => x.Corporate.TownId).GreaterThan(0).NotNull();

                RuleFor(x => x.Corporate.Address).NotEmpty().NotNull();

                RuleFor(x => x.Corporate.CompanyEmail).NotEmpty().NotNull();

                RuleFor(x => x.Corporate.CorporateMobileNumber).NotEmpty().NotNull();

                RuleFor(x => x.Corporate.IsCompanyGo).NotEmpty().NotNull();                
            });

        }
    }

    public class SimRegistrationValidators
    {
        private readonly ZamtelContext _context;

        public SimRegistrationValidators(ZamtelContext context)
        {
            _context = context;
        }

        public bool IsCustomerMobileNumberExist(string mobileNumber)
        {
            var mobileNumberExists = _context.Customers.Where(x => x.MobileNumber == mobileNumber).FirstOrDefault();

            return mobileNumberExists != null;
        }

        public bool IsCustomerIdNumberExist(string idNumber)
        {
            var idNumberExists = _context.Customers.Where(x => x.NationalIdNumber == idNumber).FirstOrDefault();

            return idNumberExists != null;
        }

        public bool IsCorporateMobileNumberExist(string mobileNumber)
        {
            var mobileNumberExists = _context.Corporates.Where(x => x.CorporateMobileNumber == mobileNumber).FirstOrDefault();

            return mobileNumberExists != null;
        }

        public bool IsCorporateIdNumberExist(string idNumber)
        {
            var idNumberExists = _context.Corporates.Where(x => x.CorporateIdNumber == idNumber).FirstOrDefault();

            return idNumberExists != null;
        }
    }
}
