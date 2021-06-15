using OAJ.WebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OAJ.WebService.Internal.Communication
{
    public interface IJudgeService
    {
        Task<string> GetTestAsync(string input);

        Task<TaskResult> AddTaskAsync(string taskName, string language, string task);
    }
}
