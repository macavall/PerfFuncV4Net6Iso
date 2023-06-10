using System;
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

            const int numberOfThreads = 300;

            Task[] tasks = new Task[numberOfThreads];

            for (int i = 0; i < numberOfThreads; i++)
            {
                tasks[i] = Task.Run(async () =>
                {
                    // Perform some work or operations within the task
                    Console.WriteLine($"Thread {Task.CurrentId} is running.");

                    // Simulate a delay of 60 seconds using Task.Delay
                    //await Task.Delay(TimeSpan.FromSeconds(60));

                    Thread.SpinWait(2147483647);

                    Console.WriteLine($"Thread {Task.CurrentId} has completed.");
                });
            }

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");

            return response;
        }

        //
    }
}
