using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PAM.Core.App.Validation;
using PAM.Core.App.RequestResolver;

namespace PAM.Core.App.Apartments.Queries
{
    public class GetApartmentsListQueryHandler : DbRequestHandler<Void, IEnumerable<ApartmentOverview>>
    {
        public GetApartmentsListQueryHandler(IPersonalAccountsDbContext dbContext) : base(dbContext) { }

        public override async Task<IEnumerable<ApartmentOverview>> Handle(Void req, CancellationToken cancellationToken)
        {
            var entities =
                DbContext.Apartments
                    .Include(apartment => apartment.Residents)
                    .Select(apartment => new ApartmentOverview(apartment));

            return entities;
        }
    }
}