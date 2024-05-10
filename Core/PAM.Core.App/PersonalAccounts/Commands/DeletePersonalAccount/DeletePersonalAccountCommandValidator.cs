using PAM.Core.App.Validation;
using PAM.Core.App.Common.Exceptions;

namespace PAM.Core.App.PersonalAccounts.Commands
{
    public class DeletePersonalAccountCommandValidator : IValidator<DeletePersonalAccountCommand>
    {
        public DeletePersonalAccountCommand Validate(DeletePersonalAccountCommand obj)
        {
            Validations.ValidatePositiveInt(obj.Id, new PersonalAccountIdValidationException());

            return obj;
        }
    }
}