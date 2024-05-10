using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PAM.Core.App.Common.Exceptions;
using PAM.Core.App.Validation;
using PAM.Core.App.RequestResolver;
using PAM.Core.Domain;

namespace PAM.Core.App.Apartments.Commands
{
    public class CreateApartmentCommandHandler : DbRequestHandler<CreateApartmentCommand, int>
    {
        public CreateApartmentCommandHandler(IPersonalAccountsDbContext dbContext) : base(dbContext) { }
        
        public override async Task<int> Handle(CreateApartmentCommand req, CancellationToken cancellationToken)
        {
            if (await DbContext.Apartments.FirstOrDefaultAsync(apartment => 
                    apartment.Address == req.Address, cancellationToken: cancellationToken) is not null)
            {
                throw new AlreadyExistsException(nameof(Apartment), nameof(Apartment.Address), req.Address);
            }
            
            var apartment = new Apartment
            {
                Address = req.Address,
                Area = req.Area
            };
            
            await DbContext.Apartments.AddAsync(apartment, cancellationToken); 
            await DbContext.SaveChangesAsync(cancellationToken);

            return apartment.Id;
        }
    }
}