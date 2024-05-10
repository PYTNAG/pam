using PAM.Core.Domain;

namespace PAM.Core.App.Residents.Queries
{
    public class PersonalAccountOverview
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Address { get; set; }

        public PersonalAccountOverview(PersonalAccount pa)
        {
            Id = pa.Id;
            Number = pa.Number;
            Address = pa.Apartment.Address;
        }
    }
}