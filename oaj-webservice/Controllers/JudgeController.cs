using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OAJ.WebService.Internal.Communication;
using OAJ.WebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpGet]
        public IActionResult Get([FromQuery]string input)
        {
            return Ok(JudgeServiceInstance.GetTest(input));
        }
        private readonly ILogger<JudgeController> _logger;
        private readonly IJudgeService JudgeServiceInstance;
    }
}
