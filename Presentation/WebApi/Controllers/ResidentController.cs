using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAM.Core.App.RequestResolver;
using PAM.Core.App.Residents.Commands;
using PAM.Core.App.Residents.Queries;

namespace PAM.WebApi.Controllers
{
    [Route("/api/residents")]
    public class ResidentController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult> Create()
        {
            var request =
                await HttpContext.Request.ReadFromJsonAsync<CreateResidentCommand>();

            var id = await RequestResolver.Resolve<CreateResidentCommand, int>(request);

            return Ok(id);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var request = new DeleteResidentCommand
            {
                Id = id
            };

            await RequestResolver.Resolve<DeleteResidentCommand, Void>(request);

            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id)
        {
            var request =
                await HttpContext.Request.ReadFromJsonAsync<UpdateResidentCommand>();

            request.Id = id;

            await RequestResolver.Resolve<UpdateResidentCommand, Void>(request);

            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ResidentDetails>> GetDetails(int id)
        {
            var request = new GetResidentDetailsQuery()
            {
                Id = id
            };

            var resident = await RequestResolver.Resolve<GetResidentDetailsQuery, ResidentDetails>(request);

            return Ok(resident);
        }
    }
}