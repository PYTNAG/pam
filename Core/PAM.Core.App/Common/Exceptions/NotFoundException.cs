using System;

namespace PAM.Core.App.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string typeName, string property, object key) 
            : base($"{typeName} with {property} {key} not found.") { }
    }
}