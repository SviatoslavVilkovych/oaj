using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OAJ.WebService.Internal.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OAJ.WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParticipantsController : ControllerBase
    {

        public ParticipantsController(IJudgeService judgeService, ILogger<ParticipantsController> logger)
        {
            _logger = logger;
            JudgeServiceInstance = judgeService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string input)
        {
            return Ok(JudgeServiceInstance.GetTest(input));
        }
        private readonly ILogger<ParticipantsController> _logger;
        private readonly IJudgeService JudgeServiceInstance;
    }
}
