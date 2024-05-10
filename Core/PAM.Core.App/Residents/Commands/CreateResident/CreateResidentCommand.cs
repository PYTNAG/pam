namespace PAM.Core.App.Residents.Commands
{
    public class CreateResidentCommand
    {
        public string Firstname { get; set; }
        public string Secondname { get; set; }
        public string Patronym { get; set; }
        public string Passport { get; set; }
        
        public int ApartmentId { get; set; }
    }
}