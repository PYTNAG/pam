using PAM.Core.Domain;

namespace PAM.Core.App.Apartments.Queries
{
    public class ApartmentOverview
    {
        public string Address { get; set; }
        public float Area { get; set; }
        public int ResidentsCount { get; set; }

        public ApartmentOverview(Apartment apartment)
        {
            Address = apartment.Address;
            Area = apartment.Area;
            ResidentsCount = apartment.Residents.Count;
        }
    }
}