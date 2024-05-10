using System;

namespace PAM.Core.App.PersonalAccounts.Commands
{
    public class UpdatePersonalAccountCommand
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime? BeginAt { get; set; }
        public DateTime? EndAt { get; set; }
        public int? ApartmentId { get; set; }

        public bool IsNull =>
            Number is null
            && BeginAt is null
            && EndAt is null
            && ApartmentId is null;
    }
}