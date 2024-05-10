using System.Threading;
using System.Threading.Tasks;
using PAM.Core.App.Common.Exceptions;
using PAM.Core.App.RequestResolver;
using PAM.Core.App.Validation;
using PAM.Core.Domain;

namespace PAM.Core.App.Residents.Queries
{
    public class GetResidentDetailsQueryHandler : DbRequestHandler<GetResidentDetailsQuery, ResidentDetails>
    {
        public GetResidentDetailsQueryHandler(IPersonalAccountsDbContext dbContext) : base(dbContext) { }

        public override async Task<ResidentDetails> Handle(GetResidentDetailsQuery req, CancellationToken cancellationToken)
        {
            var entity =
                await DbContext.Residents.FindAsync(new object[] { req.Id }, cancellationToken: cancellationToken);

            if (entity is null)
            {
                throw new NotFoundException(nameof(Resident), nameof(Resident.Id), req.Id);
            }

            return new ResidentDetails(entity);
        }
    }
}