namespace PAM.Core.App.Residents.Commands
{
    public class UpdateResidentCommand
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronym { get; set; }
        
        public string Passport { get; set; }

        public int? ApartmentId { get; set; }

        public bool IsNull =>
            FirstName is null
            && SecondName is null
            && Patronym is null
            && Passport is null
            && ApartmentId is null;
    }
}