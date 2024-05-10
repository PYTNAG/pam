using PAM.Core.App.Common.Exceptions;
using PAM.Core.App.Validation;
using PAM.Core.Domain;

namespace PAM.Core.App.Residents.Commands
{
    public class UpdateResidentCommandValidator : IValidator<UpdateResidentCommand>
    {
        public UpdateResidentCommand Validate(UpdateResidentCommand obj)
        {
            Validations.ValidatePositiveInt(obj.Id, new ResidentIdValidationException());
            
            Validations.ValidateNullableString(obj.FirstName, new StringValidationException(nameof(Resident), nameof(Resident.FirstName)));
            Validations.ValidateNullableString(obj.SecondName, new StringValidationException(nameof(Resident), nameof(Resident.SecondName)));
            Validations.ValidateNullableString(obj.Patronym, new StringValidationException(nameof(Resident), nameof(Resident.Patronym)));
            
            Validations.ValidateNullableString(obj.Passport, new StringValidationException(nameof(Resident), nameof(Resident.Passport)));
            
            Validations.ValidatePositiveInt(obj.ApartmentId, new ApartmentIdValidationException());

            return obj;
        }
    }
}