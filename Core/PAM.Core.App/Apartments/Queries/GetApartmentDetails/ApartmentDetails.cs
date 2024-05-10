using System.Collections.Generic;
using System.Linq;
using PAM.Core.App.Residents.Queries;
using PAM.Core.Domain;

namespace PAM.Core.App.Apartments.Queries
{
    public class ApartmentDetails
    {
        public string Address { get; set; }
        public float Area { get; set; }
        public List<ResidentDetails> Residents { get; set; }
        
        public ApartmentDetails(Apartment apartment)
        {
            Address = apartment.Address;
            Area = apartment.Area;
            Residents = apartment.Residents.Select(r => new ResidentDetails(r)).ToList();
        }
    }
}