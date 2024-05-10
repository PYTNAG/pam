using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PAM.Core.App.Common.Exceptions;
using PAM.Core.App.Validation;
using PAM.Core.App.RequestResolver;
using PAM.Core.Domain;

namespace PAM.Core.App.Residents.Queries
{
    public class GetPersonalAccountDetailsQueryHandler 
        : DbRequestHandler<GetPersonalAccountDetailsQuery, PersonalAccountDetails>
    {
        public GetPersonalAccountDetailsQueryHandler(IPersonalAccountsDbContext dbContext) : base(dbContext) { }
        
        public override async Task<PersonalAccountDetails> Handle(
            GetPersonalAccountDetailsQuery req, CancellationToken cancellationToken)
        {
            var entity =
                await DbContext.PersonalAccounts
                    .Include(personalAccount => personalAccount.Apartment)
                    .ThenInclude(apartment => apartment.Residents)
                    .FirstOrDefaultAsync(personalAccount => personalAccount.Id == req.Id, cancellationToken);

            if (entity is null)
            {
                throw new NotFoundException(nameof(PersonalAccount), nameof(PersonalAccount.Id), req.Id);
            }

            return new PersonalAccountDetails(entity);
        }
    }
}