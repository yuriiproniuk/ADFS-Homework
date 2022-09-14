using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web.Resource;

namespace RandomTruthApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TruthController : ControllerBase
    {
        private readonly ILogger<TruthController> _logger;

        // The Web API will only accept tokens 1) for users, and 2) having the "access_as_user" scope for this API
        static readonly string[] scopeRequiredByApi = new string[] { "access_as_user" };

        public TruthController(ILogger<TruthController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [RequiredScopeOrAppPermission(AcceptedAppPermission = new[] { "DaemonAppRole" })]
        public bool Get()
        {
            //Random gen = new Random();
            //int prob = gen.Next(100);
            //return prob <= 20;
            return true;
        }
    }
}
