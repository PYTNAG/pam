using PAM.Core.App.Common.Exceptions;
using PAM.Core.App.Validation;

namespace PAM.Core.App.Apartments.Queries
{
    public class GetApartmentDetailsQueryValidator : IValidator<GetApartmentDetailsQuery>
    {
        public GetApartmentDetailsQuery Validate(GetApartmentDetailsQuery obj)
        {
            Validations.ValidatePositiveInt(obj.Id, new ApartmentIdValidationException());

            return obj;
        }
    }
}