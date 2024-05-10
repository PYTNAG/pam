using PAM.Core.App.Common.Exceptions;
using PAM.Core.App.Validation;
using PAM.Core.Domain;

namespace PAM.Core.App.Residents.Queries
{
    public class GetPersonalAccountsListQueryValidator : IValidator<GetPersonalAccountsListQuery>
    {
        public GetPersonalAccountsListQuery Validate(GetPersonalAccountsListQuery obj)
        {
            Validations.ValidatePositiveInt(obj.Page, new PositiveIntValidationException(nameof(obj.Page)));
            Validations.ValidatePositiveInt(obj.PageCapacity, new PositiveIntValidationException(nameof(obj.PageCapacity)));

            Validations.ValidateNullableString(obj.Number, new PersonalAccountNumberValidationException());
            Validations.ValidateNullableString(obj.Address, new ApartmentAddressValidationException());
            
            Validations.ValidateNullableString(
                obj.FirstName, new StringValidationException(nameof(Resident), nameof(Resident.FirstName)));
            Validations.ValidateNullableString(
                obj.SecondName, new StringValidationException(nameof(Resident), nameof(Resident.SecondName)));
            Validations.ValidateNullableString(
                obj.Patronym, new StringValidationException(nameof(Resident), nameof(Resident.Patronym)));

            return obj;
        }
    }
}