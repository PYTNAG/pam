using System.Collections.Generic;

namespace PAM.Core.Domain
{
    public sealed class Apartment
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public float Area { get; set; }
        
        public List<Resident> Residents { get; set; }
        public List<PersonalAccount> PersonalAccounts { get; set; }
    }
}