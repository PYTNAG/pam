using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAM.Core.App.PersonalAccounts.Commands;
using PAM.Core.App.Residents.Queries;
using Void = PAM.Core.App.RequestResolver.Void;

namespace PAM.WebApi.Controllers
{
    [Route("api/personal-accounts")]
    public class PersonalAccountController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<int>> Create()
        {
            var request =
                await HttpContext.Request.ReadFromJsonAsync<CreatePersonalAccountCommand>();

            var id = await RequestResolver.Resolve<CreatePersonalAccountCommand, int>(request);

            return Ok(id);
        }
            
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonalAccountOverview>>> GetList()
        {
            var request = new GetPersonalAccountsListQuery(HttpContext.Request.Query);

            var personalAccounts = await RequestResolver
                .Resolve<GetPersonalAccountsListQuery, IEnumerable<PersonalAccountOverview>>(request);
            
            return Ok(personalAccounts);
        }
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PersonalAccountDetails>> GetDetails(int id)
        {
            var request = new GetPersonalAccountDetailsQuery
            {
                Id = id
            };

            var personalAccount = await RequestResolver
                .Resolve<GetPersonalAccountDetailsQuery, PersonalAccountDetails>(request);

            return Ok(personalAccount);
        }
        
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var request = new DeletePersonalAccountCommand
            {
                Id = id
            };

            await RequestResolver.Resolve<DeletePersonalAccountCommand, Void>(request);

            return Ok();
        }
        
        [HttpPut("{id:int}")]
        public async Task<ActionResult<int>> Update(int id)
        {
            var request = 
                await HttpContext.Request.ReadFromJsonAsync<UpdatePersonalAccountCommand>(new CancellationToken());

            request.Id = id;

            await RequestResolver.Resolve<UpdatePersonalAccountCommand, Void>(request);

            return Accepted();
        }
    }
}