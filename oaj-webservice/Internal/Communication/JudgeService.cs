using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OAJ.WebService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OAJ.WebService.Internal.Communication
{
    public class JudgeService : IJudgeService, IDisposable
    {
        public JudgeService(IConfiguration configuration, ILogger<JudgeService> logger)
        {
            Hostname = configuration["JudgeServer:Hostname"];
            Port = int.Parse(configuration["JudgeServer:Port"]);
            Client = new TcpClient(Hostname, Port);
            Stream = Client.GetStream();
            Logger = logger;
        }

        public async Task<TaskResult> CheckTaskAsync(TaskCheck input)
        {
            var x = new XmlSerializer(input.GetType());
            var s = new StringWriter();
            x.Serialize(s, input);
            var xml = s.ToString();
            await SendAsync(MessageCode.CheckTask, xml);
            return new TaskResult()
            {
                Id = Guid.NewGuid(),
                Name = "TestTask",
                Status = "Accepted"
            };
        }

        public Task<TaskResult> CheckTaskAsync1(Guid guid)
        {
            //await SendAsync(MessageCode.CheckTask, input);
            return Task.Run(() => new TaskResult()
            {
                Id = guid,
                Name = "TestTask",
                Status = "Correct"
            });
        }

        public async Task<TaskResult> AddTaskAsync(TaskEntity task)
        {
            var guid = Guid.NewGuid();
            task.Id = guid;
            RegisteredTasks.Add(guid, task);
            NamesToIdTasks.Add(task.Name, guid);

            var x = new XmlSerializer(task.GetType());
            var s = new StringWriter();
            x.Serialize(s, task);
            var xml = s.ToString();
            await SendAsync(MessageCode.AddTask, xml);

            return new TaskResult()
            {
                Id = guid,
                Name = task.Name,
                Status = "Added"
            };
        }

        private Task<string> SendAsync(MessageCode status, string input)
        {
            return Task.Run(() =>
            {
                // Translate the passed message into ASCII and store it as a Byte array.
                var statusString = System.Text.Encoding.ASCII.GetBytes(((int)status).ToString());
                var data = System.Text.Encoding.ASCII.GetBytes(input);

                Stream.Write(statusString, 0, statusString.Length);
                Stream.Write(data, 0, data.Length);
                Logger.LogInformation("Send");
                var response = new byte[256];
                Stream.Read(response, 0, 256);
                return System.Text.Encoding.ASCII.GetString(response);
            });
        }

        public void Dispose()
        {
            Stream.Dispose();
            Client.Dispose();
        }

        private readonly TcpClient Client;
        private readonly NetworkStream Stream;

        private readonly IDictionary<Guid, TaskEntity> RegisteredTasks = new Dictionary<Guid, TaskEntity>();
        private readonly IDictionary<string, Guid> NamesToIdTasks = new Dictionary<string, Guid>();

        private readonly string Hostname;
        private readonly int Port;
        private readonly ILogger Logger;

    }

    public enum MessageCode
    {
        AddTask,
        CheckTask
    }
}
