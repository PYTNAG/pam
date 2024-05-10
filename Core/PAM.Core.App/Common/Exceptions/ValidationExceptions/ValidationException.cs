using System;

namespace PAM.Core.App.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message)
            : base($"Validation exception: {message}") { }
    }
}