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
    public class TaskController : ControllerBase
    {

        public TaskController(IJudgeService judgeService, ILogger<TaskController> logger)
        {
            _logger = logger;
            JudgeServiceInstance = judgeService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string input)
        {
            return Ok(JudgeServiceInstance.GetTest(input));
        }
        private readonly ILogger<TaskController> _logger;
        private readonly IJudgeService JudgeServiceInstance;
    }
}
