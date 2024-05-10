using PAM.Core.App.Common.Exceptions;
using PAM.Core.App.Validation;

namespace PAM.Core.App.Residents.Queries
{
    public class GetPersonalAccountDetailsQueryValidator : IValidator<GetPersonalAccountDetailsQuery>
    {
        public GetPersonalAccountDetailsQuery Validate(GetPersonalAccountDetailsQuery obj)
        {
            Validations.ValidatePositiveInt(obj.Id, new PersonalAccountIdValidationException());

            return obj;
        }
    }
}