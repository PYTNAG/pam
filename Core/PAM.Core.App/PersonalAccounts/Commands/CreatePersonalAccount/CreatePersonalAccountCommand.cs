using System;

namespace PAM.Core.App.PersonalAccounts.Commands
{
    public class CreatePersonalAccountCommand
    {
        public string Number { get; set; }
        public DateTime BeginAt { get; set; }
        public DateTime EndAt { get; set; }
        public int ApartmentId { get; set; }
    }
}