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
    public class JudgeService : IJudgeService
    {
        public JudgeService(IConfiguration configuration, ILogger<JudgeService> logger)
        {
            Hostname = configuration["JudgeServer:Hostname"];
            Port = int.Parse(configuration["JudgeServer:Port"]);
            Logger = logger;
        }

        public Task<string> GetTestAsync(string input)
        {
            return SendAsync(input);
        }

        public async Task<TaskResult> AddTaskAsync(string taskName, string language, string task)
        {
            var guid = Guid.NewGuid();
            var registeredTask = new TaskEntity()
            {
                Id = guid,
                Name = taskName,
                Language = language,
                Program = task
            };
            RegisteredTasks.Add(guid, registeredTask);
            NamesToIdTasks.Add(taskName, guid);

            var x = new XmlSerializer(registeredTask.GetType());
            var s = new StringWriter();
            x.Serialize(s, registeredTask);
            var xml = s.ToString();
            await SendAsync(xml.Length.ToString());
            await SendAsync(xml.ToString());

            return new TaskResult()
            {
                Id = guid,
                Name = taskName,
                Status = ""
            };
        }

        private Task<string> SendAsync(string input)
        {
            return Task.Run(() =>
            {
                // Translate the passed message into ASCII and store it as a Byte array.
                var data = System.Text.Encoding.ASCII.GetBytes(input);

                using (var client = new TcpClient(Hostname, Port))
                using (var stream = client.GetStream())
                {
                    stream.Write(data, 0, data.Length);
                    Logger.LogInformation("Send");
                    var response = new byte[256];
                    stream.Read(response, 0, 256);
                    return System.Text.Encoding.ASCII.GetString(response);
                }
            });
        }

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
