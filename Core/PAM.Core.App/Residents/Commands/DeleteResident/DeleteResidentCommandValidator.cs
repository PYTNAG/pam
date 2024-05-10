using PAM.Core.App.Common.Exceptions;
using PAM.Core.App.Validation;

namespace PAM.Core.App.Residents.Commands
{
    public class DeleteResidentCommandValidator : IValidator<DeleteResidentCommand>
    {
        public DeleteResidentCommand Validate(DeleteResidentCommand obj)
        {
            Validations.ValidatePositiveInt(obj.Id, new ResidentIdValidationException());

            return obj;
        }
    }
}