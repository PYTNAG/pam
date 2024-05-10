namespace PAM.Core.Domain
{
    public sealed class Resident
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronym { get; set; }
        public string Passport { get; set; }
        
        public int ApartmentId { get; set; }
        public Apartment Apartment { get; set; }
    }
}