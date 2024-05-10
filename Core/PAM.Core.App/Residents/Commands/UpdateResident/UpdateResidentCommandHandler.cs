using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PAM.Core.App.Common.Exceptions;
using PAM.Core.App.RequestResolver;
using PAM.Core.App.Validation;
using PAM.Core.Domain;
using Void = PAM.Core.App.RequestResolver.Void;

namespace PAM.Core.App.Residents.Commands
{
    public class UpdateResidentCommandHandler : DbRequestHandler<UpdateResidentCommand, Void>
    {
        public UpdateResidentCommandHandler(IPersonalAccountsDbContext dbContext) : base(dbContext) { }

        public override async Task<Void> Handle(UpdateResidentCommand req, CancellationToken cancellationToken)
        {
            if (req.IsNull)
            {
                return new Void();
            }

            var entity =
                await DbContext.Residents
                    .FindAsync(new object[] { req.Id }, cancellationToken: cancellationToken);

            if (entity is null)
            {
                throw new NotFoundException(nameof(Resident), nameof(Resident.Id), req.Id);
            }
            
            if (req.Passport is not null)
            {
                if (await DbContext.Residents.AnyAsync(resident => resident.Passport == req.Passport,
                        cancellationToken))
                {
                    throw new AlreadyExistsException(
                        nameof(Resident), nameof(Resident.Passport), req.Passport);
                }
            }

            if (req.ApartmentId is not null)
            {
                if (await DbContext.Apartments
                        .FindAsync(new object[]{ req.ApartmentId }, cancellationToken: cancellationToken) is null)
                {
                    throw new NotFoundException(nameof(Apartment), nameof(Apartment.Id), req.ApartmentId);
                }
            }

            entity.FirstName = req.FirstName ?? entity.FirstName;
            entity.SecondName = req.SecondName ?? entity.SecondName;
            entity.Patronym = req.Patronym ?? entity.Patronym;
            entity.Passport = req.Passport ?? entity.Passport;
            entity.ApartmentId = req.ApartmentId ?? entity.ApartmentId;

            await DbContext.SaveChangesAsync(cancellationToken);

            return new Void();
        }
    }
}