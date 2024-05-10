using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PAM.Core.App.Common.Exceptions;
using PAM.Core.App.Validation;
using PAM.Core.App.RequestResolver;
using PAM.Core.Domain;

namespace PAM.Core.App.PersonalAccounts.Commands
{
    public class CreatePersonalAccountCommandHandler : DbRequestHandler<CreatePersonalAccountCommand, int>
    {
        public CreatePersonalAccountCommandHandler(IPersonalAccountsDbContext dbContext) : base(dbContext) { }
        
        public override async Task<int> Handle(CreatePersonalAccountCommand req, CancellationToken cancellationToken)
        {
            if (await DbContext.PersonalAccounts.FirstOrDefaultAsync(personalAccount =>
                    personalAccount.Number == req.Number, cancellationToken) is not null)
            {
                throw new AlreadyExistsException(nameof(PersonalAccount), nameof(PersonalAccount.Number), req.Number);
            }

            if (await DbContext.Apartments.FindAsync(new object[] { req.ApartmentId }, cancellationToken) is null)
            {
                throw new NotFoundException(nameof(Apartment), nameof(Apartment.Id), req.ApartmentId);
            }
            
            var personalAccount = new PersonalAccount
            {
                Number = req.Number,
                BeginAt = req.BeginAt,
                EndAt = req.EndAt,
                ApartmentId = req.ApartmentId
            };
            
            await DbContext.PersonalAccounts.AddAsync(personalAccount, cancellationToken); 
            await DbContext.SaveChangesAsync(cancellationToken);
            
            return personalAccount.Id;
        }
    }
}