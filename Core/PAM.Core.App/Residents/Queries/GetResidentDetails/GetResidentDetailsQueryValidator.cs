using PAM.Core.App.Common.Exceptions;
using PAM.Core.App.Validation;

namespace PAM.Core.App.Residents.Queries
{
    public class GetResidentDetailsQueryValidator : IValidator<GetResidentDetailsQuery>
    {
        public GetResidentDetailsQuery Validate(GetResidentDetailsQuery obj)
        {
            Validations.ValidatePositiveInt(obj.Id, new ResidentIdValidationException());

            return obj;
        }
    }
}