using System;
using PAM.Core.App.Common.Exceptions;

namespace PAM.Core.App.Validation
{
    public static class Validations
    {
        public static void ValidatePositiveInt(int value, PositiveIntValidationException exception)
        {
            if (value < 1)
            {
                throw exception;
            }
        }
        
        public static void ValidatePositiveInt(int? value, PositiveIntValidationException exception)
        {
            if (value is null)
            {
                return;
            }

            ValidatePositiveInt((int)value, exception);
        }

        public static void ValidateString(string str, StringValidationException exception)
        {
            if (str is null or "")
            {
                throw exception;
            }
        }

        public static void ValidateNullableString(string str, StringValidationException exception)
        {
            if (str is "")
            {
                throw exception;
            }
        }

        public static void ValidateArea(float area)
        {
            if (area <= 0)
            {
                throw new ValidationException("Area must be positive.");
            }
        }

        public static void ValidatePeriod(DateTime begin, DateTime end)
        {
            if (end < begin)
            {
                throw new OpenPeriodValidationException();
            }
        }
    }
}