using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PAM.Core.App.Common.Exceptions;
using PAM.Core.App.Validation;
using PAM.Core.App.RequestResolver;
using PAM.Core.Domain;

namespace PAM.Core.App.PersonalAccounts.Commands
{
    public class UpdatePersonalAccountCommandHandler : DbRequestHandler<UpdatePersonalAccountCommand>
    {
        public UpdatePersonalAccountCommandHandler(IPersonalAccountsDbContext dbContext) : base(dbContext) { }
        
        public override async Task<Void> Handle(UpdatePersonalAccountCommand req, CancellationToken cancellationToken)
        {
            if (req.IsNull)
            {
                return new Void();
            }
            
            var entity =
                await DbContext.PersonalAccounts
                    .FirstOrDefaultAsync(personalAccount => personalAccount.Id == req.Id, cancellationToken);

            if (entity is null)
            {
                throw new NotFoundException(
                    nameof(PersonalAccount), nameof(PersonalAccount.Id), req.Id);
            }

            if (req.Number is not null)
            {
                if (await DbContext.PersonalAccounts
                        .AnyAsync(personalAccount => personalAccount.Number == req.Number, cancellationToken))
                {
                    throw new AlreadyExistsException(
                        nameof(PersonalAccount), nameof(PersonalAccount.Number), req.Number);
                }
            }

            entity.Number = req.Number ?? entity.Number;
            entity.BeginAt = req.BeginAt ?? entity.BeginAt;
            entity.EndAt = req.EndAt ?? entity.EndAt;

            if (entity.EndAt < entity.BeginAt)
            {
                throw new OpenPeriodValidationException();
            }

            await DbContext.SaveChangesAsync(cancellationToken);

            return new Void();
        }
    }
}