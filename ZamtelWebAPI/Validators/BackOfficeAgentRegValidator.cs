using FluentValidation;
using ZamtelWebAPI.ViewModels;

namespace ZamtelWebAPI.Validators
{
    public class BackOfficeAgentRegValidator : AbstractValidator<BackOfficeAgentRegistration>
    {
        public BackOfficeAgentRegValidator()
        {
            RuleFor(x => x.Portrait).NotEmpty().NotNull();

            RuleFor(x => x.NationalIdFront).NotEmpty().NotNull();

            RuleFor(x => x.NationalIdBack).NotEmpty().NotNull();

            RuleFor(x => x.AgentContractForm).NotEmpty().NotNull();

            RuleFor(x => x.AgentRegistrationDetails).NotNull();
        }
    }
}
