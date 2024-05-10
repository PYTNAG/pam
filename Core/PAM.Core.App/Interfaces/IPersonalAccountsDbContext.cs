using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PAM.Core.Domain;

namespace PAM.Core.App.Validation
{
    public interface IPersonalAccountsDbContext
    {
        DbSet<PersonalAccount> PersonalAccounts { get; }
        DbSet<Apartment> Apartments { get; }
        DbSet<Resident> Residents { get; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}