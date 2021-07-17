using OAJ.WebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OAJ.WebService.Internal.Communication
{
    public interface IJudgeService
    {
        Task<TaskResult> CheckTaskAsync1(Guid input);
        Task<TaskResult> CheckTaskAsync(TaskCheck input);

        Task<TaskResult> AddTaskAsync(TaskEntity task);
    }
}
