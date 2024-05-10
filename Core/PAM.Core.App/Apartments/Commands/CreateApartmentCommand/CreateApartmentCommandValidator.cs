using PAM.Core.App.Common.Exceptions;
using PAM.Core.App.Validation;

namespace PAM.Core.App.Apartments.Commands
{
    public class CreateApartmentCommandValidator : IValidator<CreateApartmentCommand>
    {
        public CreateApartmentCommand Validate(CreateApartmentCommand obj)
        {
            Validations.ValidateString(obj.Address, new ApartmentAddressValidationException());
            Validations.ValidateArea(obj.Area);

            return obj;
        }
    }
}