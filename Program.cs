using Microsoft.Extensions.Hosting;

namespace PerfFuncV4Net6Iso
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()  // fixing formatting
                .ConfigureFunctionsWorkerDefaults()
                .Build();

            host.Run();
        }
    }
}