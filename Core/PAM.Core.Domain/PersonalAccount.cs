using System;

namespace PAM.Core.Domain
{
    public sealed class PersonalAccount
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime BeginAt { get; set; }
        public DateTime EndAt { get; set; }
        
        public int ApartmentId { get; set; }
        public Apartment Apartment { get; set; }
    }
}