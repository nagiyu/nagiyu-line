using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using SettingsManager.Services;
using SettingsRepository;

namespace SettingsManager.Tests.Services
{
    [TestClass]
    public class AppSettingsServiceTest
    {
        private AppDbContext context;
        private IConfiguration configuration;
        private readonly AppSettingsService appSettingsService;

        public AppSettingsServiceTest()
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
        }

        [TestMethod]
        public async Task GetValueByKeyAsync_String()
        {
            var value = await appSettingsService.GetValueByKeyAsync("OpenAI:APIKey");
            Debug.WriteLine($"Key: {value}");
            Assert.IsNotNull(value);
        }

        [TestMethod]
        public async Task GetValueByKeyAsync_String_NotExistKey()
        {
            var value = await appSettingsService.GetValueByKeyAsync("DummyKey");
            Debug.WriteLine($"Key: {value}");
            Assert.IsNull(value);
        }

        [TestMethod]
        public async Task GetValueByKeyAsync_Int()
        {
            var value = await appSettingsService.GetValueByKeyAsync<int>("AWS:DynamoDB:MaxMessages");
            Debug.WriteLine($"Key: {value}");
            Assert.IsNotNull(value);
        }

        [TestMethod]
        public async Task GetValueByKeyAsync_Int_NotExistKey()
        {
            var value = await appSettingsService.GetValueByKeyAsync<int>("DummyKey");
            Debug.WriteLine($"Key: {value}");
            Assert.AreEqual(0, value);
        }
    }
}
