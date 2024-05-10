using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PAM.Core.App.Validation;
using PAM.Core.App.RequestResolver;

namespace PAM.Core.App.Residents.Queries
{
    public class GetPersonalAccountsListQueryHandler 
        : DbRequestHandler<GetPersonalAccountsListQuery, IEnumerable<PersonalAccountOverview>>
    {
        public GetPersonalAccountsListQueryHandler(IPersonalAccountsDbContext dbContext) : base(dbContext) { }
        
        public override async Task<IEnumerable<PersonalAccountOverview>> Handle(
            GetPersonalAccountsListQuery req, CancellationToken cancellationToken)
        {
            var filteredAccounts =
                DbContext.PersonalAccounts
                    .Include(pa => pa.Apartment)
                    .ThenInclude(a => a.Residents)
                    .AsQueryable();

            if (!filteredAccounts.Any())
            {
                return new List<PersonalAccountOverview>();
            }
            
            // if number is specified then the other filters have no meaning
            if (req.Number is not null)
            {
                filteredAccounts = filteredAccounts.Where(personalAccount => personalAccount.Number == req.Number);
            }
            else if (!req.IsNull)
            {
                if (req.FirstName is not null)
                {
                    filteredAccounts = 
                        filteredAccounts.Where(personalAccount => 
                            personalAccount.Apartment.Residents.Any(resident => resident.FirstName == req.FirstName));
                }

                if (req.SecondName is not null)
                {
                    filteredAccounts = 
                        filteredAccounts.Where(personalAccount => 
                            personalAccount.Apartment.Residents.Any(resident => resident.SecondName == req.SecondName));
                }
                
                if (req.Patronym is not null)
                {
                    filteredAccounts = 
                        filteredAccounts.Where(personalAccount => 
                            personalAccount.Apartment.Residents.Any(resident => resident.Patronym == req.Patronym));
                }
                
                if (req.WithResidents)
                {
                    filteredAccounts = 
                        filteredAccounts.Where(personalAccount => personalAccount.Apartment.Residents.Any());
                }

                if (req.Address is not null)
                {
                    filteredAccounts = 
                        filteredAccounts.Where(personalAccount => personalAccount.Apartment.Address == req.Address);
                }

                if (req.OpenAt is not null)
                {
                    filteredAccounts = 
                        filteredAccounts.Where(personalAccount =>
                            personalAccount.BeginAt <= req.OpenAt && req.OpenAt < personalAccount.EndAt);
                }
            }
            
            var entities = 
                filteredAccounts
                    .OrderBy(pa => pa.Id)
                    .Skip((req.Page - 1) * req.PageCapacity)
                    .Take(req.PageCapacity);

            return entities.Select(pa => new PersonalAccountOverview(pa));
        }
    }
}