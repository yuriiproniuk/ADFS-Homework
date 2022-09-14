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
