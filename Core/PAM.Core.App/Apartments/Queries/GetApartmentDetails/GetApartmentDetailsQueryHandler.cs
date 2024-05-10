using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PAM.Core.App.Common.Exceptions;
using PAM.Core.App.Validation;
using PAM.Core.App.RequestResolver;
using PAM.Core.Domain;

namespace PAM.Core.App.Apartments.Queries
{
    public class GetApartmentDetailsQueryHandler : DbRequestHandler<GetApartmentDetailsQuery, ApartmentDetails>
    {
        public GetApartmentDetailsQueryHandler(IPersonalAccountsDbContext dbContext) : base(dbContext) { }

        public override async Task<ApartmentDetails> Handle(GetApartmentDetailsQuery req, CancellationToken cancellationToken)
        {
            var entity =
                await DbContext.Apartments
                    .Include(apartment => apartment.Residents)
                    .FirstOrDefaultAsync(apartment => apartment.Id == req.Id, cancellationToken);

            if (entity is null)
            {
                throw new NotFoundException(nameof(Apartment), nameof(Apartment.Id), req.Id);
            }

            return new ApartmentDetails(entity);
        }
    }
}