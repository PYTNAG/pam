using System;

namespace PAM.Core.App.Common.Exceptions
{
    public class DeleteRestrictedException : Exception
    {
        public DeleteRestrictedException(string typeName, string existingProperty)
            : base($"{typeName} can't be deleted while it has {existingProperty}.") { }
    }
}