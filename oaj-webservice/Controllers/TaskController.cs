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

        /// <summary>
        /// Adds new task to list of available tasks.
        /// </summary>
        /// <param name="taskName">Name of the task.</param>
        /// <param name="language">Allowed languages.</param>
        /// <param name="task">Task with required verification, limits, participants, etc.</param>
        /// <returns>Filled location with place where task stored.</returns>
        [HttpPost]
        [Route("[action]")]
        public IActionResult Add([FromQuery] string taskName, [FromQuery] string language, [FromBody] string task)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Replace available task.
        /// </summary>
        /// <param name="taskName">Name of the task.</param>
        /// <param name="language">New allowed languages.</param>
        /// <param name="task">Task with required verification, limits, participants, etc.</param>
        /// <returns>Filled location with place where new task stored.</returns>
        [HttpPut]
        [Route("[action]/{taskName}")]
        public IActionResult Replace([FromRoute] string taskName, [FromQuery] string language, [FromBody] string task)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Modify available task.
        /// </summary>
        /// <param name="taskName">Name of the task.</param>
        /// <param name="language">New allowed languages.</param>
        /// <param name="task">Task with required verification, limits, participants, etc.</param>
        /// <returns>Filled location with place where new task stored.</returns>
        [HttpPatch]
        [Route("[action]/{taskName}")]
        public IActionResult Update([FromRoute] string taskName, [FromQuery] string language, [FromBody] string task)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieve information about the task.
        /// </summary>
        /// <param name="taskName">Name of the task.</param>
        /// <returns>Description of the task.</returns>
        [HttpGet]
        [Route("[action]/{taskName}")]
        public IActionResult Get([FromQuery] string taskName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes task.
        /// </summary>
        /// <param name="taskName">Name of the task.</param>
        /// <returns>Successful code if operation was sucessful, otherwise 404NotFound code.</returns>
        [HttpGet]
        [Route("[action]/{taskName}")]
        public IActionResult Remove([FromQuery] string taskName)
        {
            throw new NotImplementedException();
        }

        private readonly ILogger<TaskController> _logger;
        private readonly IJudgeService JudgeServiceInstance;
    }
}
