using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PAM.Core.App.Common.Exceptions;
using PAM.Core.App.Validation;
using PAM.Core.App.RequestResolver;
using PAM.Core.Domain;

namespace PAM.Core.App.Residents.Commands
{
    public class CreateResidentCommandHandler : DbRequestHandler<CreateResidentCommand, int>
    {
        public CreateResidentCommandHandler(IPersonalAccountsDbContext dbContext) : base(dbContext) { }

        public override async Task<int> Handle(CreateResidentCommand req, CancellationToken cancellationToken)
        {
            if (await DbContext.Residents.AnyAsync(resident => 
                    resident.Passport == req.Passport, cancellationToken))
            {
                throw new AlreadyExistsException(nameof(Resident), nameof(Resident.Passport), req.Passport);
            }

            if (await DbContext.Apartments
                    .FindAsync(new object[] { req.ApartmentId }, cancellationToken) is null)
            {
                throw new NotFoundException(nameof(Apartment), nameof(Apartment.Id), req.ApartmentId);
            }
            
            var resident = new Resident()
            {
                FirstName = req.Firstname,
                SecondName = req.Secondname,
                Patronym = req.Patronym,
                Passport = req.Passport,
                ApartmentId = req.ApartmentId
            };

            await DbContext.Residents.AddAsync(resident, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);

            return resident.Id;
        }
    }
}