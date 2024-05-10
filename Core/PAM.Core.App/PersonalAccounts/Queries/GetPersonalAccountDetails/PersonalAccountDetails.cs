using System;
using PAM.Core.App.Apartments.Queries;
using PAM.Core.Domain;

namespace PAM.Core.App.Residents.Queries
{
    public class PersonalAccountDetails
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime BeginAt { get; set; }
        public DateTime EndAt { get; set; }
        public ApartmentDetails Apartment { get; set; }

        public PersonalAccountDetails(PersonalAccount pa)
        {
            Id = pa.Id;
            Number = pa.Number;
            BeginAt = pa.BeginAt;
            EndAt = pa.EndAt;
            Apartment = new ApartmentDetails(pa.Apartment);
        }
    }
}