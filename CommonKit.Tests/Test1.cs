using System.Diagnostics;
using CommonKit.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SettingsManager.Services;
using SettingsRepository;

namespace CommonKit.Tests
{
    [TestClass]
    public class Test1
    {
        /// <summary>
        /// AppSettingsService
        /// </summary>
        private readonly AppSettingsService appSettingsService;

        private AppDbContext context;
        private IConfiguration configuration;

        private LogService logService;

        public Test1()
        {
            var basePath = Directory.GetCurrentDirectory();
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            configuration = builder.Build();

            var connectionString = configuration.GetConnectionString("SettingsDBConnection");
            Debug.WriteLine($"Connection String: {connectionString}");

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql(connectionString)
                .Options;
            context = new AppDbContext(options);

            appSettingsService = new AppSettingsService(context);

            logService = new LogService(appSettingsService);
        }

        [TestMethod]
        public async Task TestMethod1()
        {
            await logService.WriteLogAsync("This is a test log message.");
        }
    }
}
