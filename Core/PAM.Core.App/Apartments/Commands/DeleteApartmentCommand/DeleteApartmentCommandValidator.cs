using PAM.Core.App.Common.Exceptions;
using PAM.Core.App.Validation;

namespace PAM.Core.App.Apartments.Commands
{
    public class DeleteApartmentCommandValidator : IValidator<DeleteApartmentCommand>
    {
        public DeleteApartmentCommand Validate(DeleteApartmentCommand obj)
        {
            Validations.ValidatePositiveInt(obj.Id, new ApartmentIdValidationException());

            return obj;
        }
    }
}