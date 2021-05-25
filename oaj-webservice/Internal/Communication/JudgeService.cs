using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

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

        public string GetTest(string input)
        {
            return Send(input);
        }

        private string Send(string input)
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
        }

        private readonly string Hostname;
        private readonly int Port;
        private readonly ILogger Logger;
    }
}
