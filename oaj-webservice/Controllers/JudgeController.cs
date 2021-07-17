using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OAJ.WebService.Internal.Communication;
using System;

namespace OAJ.WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JudgeController : ControllerBase
    {

        public JudgeController(IJudgeService judgeService, ILogger<JudgeController> logger)
        {
            _logger = logger;
            JudgeServiceInstance = judgeService;
        }

        [HttpPost]
        public IActionResult Post([FromBody]string input)
        {
            throw new NotImplementedException();
        }
        private readonly ILogger<JudgeController> _logger;
        private readonly IJudgeService JudgeServiceInstance;
    }
}
