using CommonKit.Services;
using Microsoft.Extensions.Configuration;

namespace CommonKit.Tests
{
    [TestClass]
    public class Test1
    {
        private LogService logService;

        public Test1()
        {
            var basePath = Directory.GetCurrentDirectory();
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();

            logService = new LogService(configuration);
        }

        [TestMethod]
        public async Task TestMethod1()
        {
            await logService.WriteLogAsync("This is a test log message.");
        }
    }
}
