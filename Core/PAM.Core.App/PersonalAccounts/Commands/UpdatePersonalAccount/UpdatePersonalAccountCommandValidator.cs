using System;
using PAM.Core.App.Common.Exceptions;
using PAM.Core.App.Validation;

namespace PAM.Core.App.PersonalAccounts.Commands
{
    public class UpdatePersonalAccountCommandValidator : IValidator<UpdatePersonalAccountCommand>
    {
        public UpdatePersonalAccountCommand Validate(UpdatePersonalAccountCommand obj)
        {
            if (obj.ApartmentId is not null)
            {
                Validations.ValidatePositiveInt((int)obj.ApartmentId, new ApartmentIdValidationException());
            }
            
            Validations.ValidateNullableString(obj.Number, new PersonalAccountNumberValidationException());
            Validations.ValidatePositiveInt(obj.Id, new PersonalAccountIdValidationException());
            
            if (obj.EndAt is not null && obj.BeginAt is not null)
            {
                Validations.ValidatePeriod((DateTime)obj.BeginAt, (DateTime)obj.EndAt);
            }

            return obj;
        }
    }
}