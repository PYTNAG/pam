using System.Threading;
using System.Threading.Tasks;
using PAM.Core.App.Common.Exceptions;
using PAM.Core.App.RequestResolver;
using PAM.Core.App.Validation;
using PAM.Core.Domain;

namespace PAM.Core.App.Residents.Commands
{
    public class DeleteResidentCommandHandler : DbRequestHandler<DeleteResidentCommand>
    {
        public DeleteResidentCommandHandler(IPersonalAccountsDbContext dbContext) : base(dbContext) { }

        public override async Task<Void> Handle(DeleteResidentCommand req, CancellationToken cancellationToken)
        {
            var entity =
                await DbContext.Residents.FindAsync(req.Id);

            if (entity is null)
            {
                throw new NotFoundException(nameof(Resident), nameof(Resident.Id), req.Id);
            }

            DbContext.Residents.Remove(entity);
            await DbContext.SaveChangesAsync(cancellationToken);

            return new Void();
        }
    }
}