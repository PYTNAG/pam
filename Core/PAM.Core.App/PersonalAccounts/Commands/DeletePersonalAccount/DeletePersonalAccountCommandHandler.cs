using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PAM.Core.App.Common.Exceptions;
using PAM.Core.App.Validation;
using PAM.Core.App.RequestResolver;
using PAM.Core.Domain;
using Void = PAM.Core.App.RequestResolver.Void;

namespace PAM.Core.App.PersonalAccounts.Commands
{
    public class DeletePersonalAccountCommandHandler : DbRequestHandler<DeletePersonalAccountCommand>
    {
        public DeletePersonalAccountCommandHandler(IPersonalAccountsDbContext dbContext) : base(dbContext) { }
        
        public override async Task<Void> Handle(DeletePersonalAccountCommand req, CancellationToken cancellationToken)
        {
            var entity =
                await DbContext.PersonalAccounts
                    .FindAsync(new object[] { req.Id }, cancellationToken);

            if (entity is null)
            {
                throw new NotFoundException(nameof(PersonalAccount), nameof(PersonalAccount.Id), req.Id);
            }

            DbContext.PersonalAccounts.Remove(entity);
            await DbContext.SaveChangesAsync(cancellationToken);

            return new Void();
        }
    }
}