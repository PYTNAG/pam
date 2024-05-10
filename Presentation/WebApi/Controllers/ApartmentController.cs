using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAM.Core.App.Apartments.Commands;
using PAM.Core.App.Apartments.Queries;
using PAM.Core.App.RequestResolver;

namespace PAM.WebApi.Controllers
{
    [Route("/api/apartments")]
    public class ApartmentController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<int>> Create()
        {
            var request =
                await HttpContext.Request.ReadFromJsonAsync<CreateApartmentCommand>();

            var id = await RequestResolver.Resolve<CreateApartmentCommand, int>(request);

            return Ok(id);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var request = new DeleteApartmentCommand
            {
                Id = id
            };

            await RequestResolver.Resolve<DeleteApartmentCommand, Void>(request);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApartmentOverview>>> GetList()
        {
            var apartments = 
                await RequestResolver.Resolve<Void, IEnumerable<ApartmentOverview>>(new Void());

            return Ok(apartments);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApartmentDetails>> GetDetails(int id)
        {
            var request = new GetApartmentDetailsQuery
            {
                Id = id
            };

            var details = await RequestResolver.Resolve<GetApartmentDetailsQuery, ApartmentDetails>(request);
            
            return Ok(details);
        }
    }
}