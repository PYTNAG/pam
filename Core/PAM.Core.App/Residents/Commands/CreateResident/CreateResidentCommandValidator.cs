using PAM.Core.App.Common.Exceptions;
using PAM.Core.App.Validation;
using PAM.Core.Domain;

namespace PAM.Core.App.Residents.Commands
{
    public class CreateResidentCommandValidator : IValidator<CreateResidentCommand>
    {
        public CreateResidentCommand Validate(CreateResidentCommand obj)
        {
            Validations.ValidateString(obj.Firstname, new StringValidationException(nameof(Resident), nameof(Resident.FirstName)));
            Validations.ValidateString(obj.Secondname, new StringValidationException(nameof(Resident), nameof(Resident.SecondName)));

            if (obj.Patronym is not null)
            {
                Validations.ValidateString(obj.Patronym, new StringValidationException(nameof(Resident), nameof(Resident.Patronym)));
            }

            Validations.ValidateString(obj.Passport, new StringValidationException(nameof(Resident), nameof(Resident.Passport)));
            
            Validations.ValidatePositiveInt(obj.ApartmentId, new ApartmentIdValidationException());

            return obj;
        }
    }
}