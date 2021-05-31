using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OAJ.WebService.Internal.Communication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OAJ.WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SystemController : ControllerBase
    {

        public SystemController(IJudgeService judgeService, ILogger<SystemController> logger)
        {
            _logger = logger;
            JudgeServiceInstance = judgeService;
        }

        /// <summary>
        /// Retrieve all settings of the system.
        /// </summary>
        /// <param name="setting">Specific setting that needs to be returned.</param>
        /// <returns>JSON-formatted settings with available languages, their configurations, etc.</returns>
        [HttpGet]
        [Route("[action]")]
        public IActionResult Settings([FromQuery] string setting)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Modify specific setting of the system.
        /// </summary>
        /// <param name="setting">Specific setting that needs to be updated..</param>
        /// <param name="settingValue">New value of the setting.</param>
        /// <returns>Updated setting.</returns>
        [HttpPatch]
        [Route("[action]")]
        public IActionResult Settings([Required][FromQuery] string setting, [Required][FromBody] string settingValue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieve all logs of the system.
        /// </summary>
        /// <param name="startDate">Date of the first interested log (if empty, returned from the beginning).</param>
        /// <param name="endDate">Date of the last interested log (if empty, returned current date).</param>
        /// <returns>Logs.</returns>
        [HttpGet]
        [Route("[action]")]
        public IActionResult Logs([FromQuery] string startDate, [FromQuery] string endDate)
        {
            throw new NotImplementedException();
        }

        private readonly ILogger<SystemController> _logger;
        private readonly IJudgeService JudgeServiceInstance;
    }
}
