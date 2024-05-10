namespace PAM.Persistence
{
    public static class DbInitializer
    {
        public static void Initialize(PersonalAccountsDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}