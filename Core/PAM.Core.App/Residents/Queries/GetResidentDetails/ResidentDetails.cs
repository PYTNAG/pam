using PAM.Core.Domain;

namespace PAM.Core.App.Residents.Queries
{
    public class ResidentDetails
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Passport { get; set; }
        
        public ResidentDetails(Resident resident)
        {
            Id = resident.Id;
            FullName = $"{resident.FirstName} {resident.SecondName}";
            if (resident.Patronym is not null)
            {
                FullName += " " + resident.Patronym;
            }
            Passport = resident.Passport;
        }
    }
}