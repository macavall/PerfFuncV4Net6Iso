using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace PerfFuncV4Net6Iso
{
    public class httpexample
    {
        private readonly ILogger _logger;

        public httpexample(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<httpexample>();
        }

        [Function("httpexample")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            await Task.Delay(1);

            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var tasks = new List<Thread>();

            for (int x = 0; x < 100; x++)
            {
                // Create detached thread
                tasks.Add(new Thread (async () =>
                {
                    await Task.Delay(5000);
                }));
            }

            // Start all threads
            foreach (var task in tasks)
            {
                task.Start();
            }

            // Wait for all threads to complete
            foreach (var task in tasks)
            {
                task.Join();
            }

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");

            return response;
        }

        //
    }
}
