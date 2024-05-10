using Microsoft.AspNetCore.Mvc;
using PAM.Core.App.RequestResolver;
using Microsoft.Extensions.DependencyInjection;

namespace PAM.WebApi.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        private RequestResolver _requestResolver;

        protected RequestResolver RequestResolver =>
            _requestResolver ??= HttpContext.RequestServices.GetService<RequestResolver>();
    }
}