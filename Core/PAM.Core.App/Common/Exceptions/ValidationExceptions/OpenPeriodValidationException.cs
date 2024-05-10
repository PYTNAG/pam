namespace PAM.Core.App.Common.Exceptions
{
    public class OpenPeriodValidationException : ValidationException
    {
        public OpenPeriodValidationException()
            : base("Open period's end-date must be after begin-date.") { }
    }
}