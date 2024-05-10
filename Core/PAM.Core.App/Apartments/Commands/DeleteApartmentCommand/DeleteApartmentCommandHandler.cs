using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PAM.Core.App.Common.Exceptions;
using PAM.Core.App.Validation;
using PAM.Core.App.RequestResolver;
using PAM.Core.Domain;

namespace PAM.Core.App.Apartments.Commands
{
    public class DeleteApartmentCommandHandler : DbRequestHandler<DeleteApartmentCommand>
    {
        public DeleteApartmentCommandHandler(IPersonalAccountsDbContext dbContext) : base(dbContext) { }
        
        public override async Task<Void> Handle(DeleteApartmentCommand req, CancellationToken cancellationToken)
        {
            var entity =
                await DbContext.Apartments
                    .Include(apartment => apartment.Residents)
                    .Include(apartment => apartment.PersonalAccounts)
                    .FirstOrDefaultAsync(apartment => apartment.Id == req.Id, cancellationToken);

            if (entity is null)
            {
                throw new NotFoundException(nameof(Apartment), nameof(Apartment.Id), req.Id);
            }

            if (entity.PersonalAccounts.Any())
            {
                throw new DeleteRestrictedException(nameof(Apartment), nameof(Apartment.PersonalAccounts));
            }

            if (entity.Residents.Any())
            {
                throw new DeleteRestrictedException(nameof(Apartment), nameof(Apartment.Residents));
            }

            DbContext.Apartments.Remove(entity);
            await DbContext.SaveChangesAsync(cancellationToken);

            return new Void();
        }
    }
}