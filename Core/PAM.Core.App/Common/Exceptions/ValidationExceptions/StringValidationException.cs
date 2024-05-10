namespace PAM.Core.App.Common.Exceptions
{
    public class StringValidationException : ValidationException
    {
        public StringValidationException(string propertyOwner, string property)
            : base($"{propertyOwner}'s {property} can't be empty string") { }
    }
    
    public class PersonalAccountNumberValidationException : StringValidationException
    {
        public PersonalAccountNumberValidationException()
            : base("Personal account", "number") { }
    }

    public class ApartmentAddressValidationException : StringValidationException
    {
        public ApartmentAddressValidationException()
            : base("Apartment", "address") { }
    }
}