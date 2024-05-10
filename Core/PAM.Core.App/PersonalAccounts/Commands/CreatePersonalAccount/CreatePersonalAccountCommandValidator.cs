using PAM.Core.App.Validation;
using PAM.Core.App.Common.Exceptions;

namespace PAM.Core.App.PersonalAccounts.Commands
{
    public class CreatePersonalAccountCommandValidator : IValidator<CreatePersonalAccountCommand>
    {
        public CreatePersonalAccountCommand Validate(CreatePersonalAccountCommand obj)
        {
            Validations.ValidatePeriod(obj.BeginAt, obj.EndAt);
            Validations.ValidateString(obj.Number, new PersonalAccountNumberValidationException());
            Validations.ValidatePositiveInt(obj.ApartmentId, new ApartmentIdValidationException());

            return obj;
        }
    }
}