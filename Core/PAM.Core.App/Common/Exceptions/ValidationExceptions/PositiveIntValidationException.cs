using PAM.Core.Domain;

namespace PAM.Core.App.Common.Exceptions
{
    public class PositiveIntValidationException : ValidationException
    {
        public PositiveIntValidationException(string propertyName)
            : base($"{propertyName} must be positive integer.") { }
    }

    public class PersonalAccountIdValidationException : PositiveIntValidationException
    {
        public PersonalAccountIdValidationException()
            : base($"{nameof(PersonalAccount)}'s {nameof(PersonalAccount.Id)}") { }
    }

    public class ApartmentIdValidationException : PositiveIntValidationException
    {
        public ApartmentIdValidationException()
            : base($"{nameof(Apartment)}'s {nameof(Apartment.Id)}") { }
    }

    public class ResidentIdValidationException : PositiveIntValidationException
    {
        public ResidentIdValidationException()
            : base($"{nameof(Resident)}'s {nameof(Resident.Id)}") { }
    }
}