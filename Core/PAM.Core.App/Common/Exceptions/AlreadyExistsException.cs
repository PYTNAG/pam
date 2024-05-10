using System;

namespace PAM.Core.App.Common.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string typeName, string property, object key)
            : base($"Record {typeName} with {property} {key} already exists.") { }
    }
}