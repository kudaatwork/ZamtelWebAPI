using FluentValidation;
using ZamtelWebAPI.ViewModels;

namespace ZamtelWebAPI.Validators
{
    public class SimRegValidator : AbstractValidator<SimRegistration>
    {
        public SimRegValidator()
        {
            RuleFor(x => x.Portrait).NotEmpty().NotNull();

            RuleFor(x => x.NationalIdFront).NotEmpty().NotNull();

            RuleFor(x => x.NationalIdBack).NotEmpty().NotNull();

            RuleFor(x => x.SimRegistrationDetails).NotNull();
        }
    }
}
