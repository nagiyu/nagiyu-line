using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using CommonKit.Utilities;

using SettingsManager.Services;
using SettingsRepository;

using OpenAIConnect.Common.Interfaces;
using OpenAIConnect.Common.Models.Request;

using static OpenAIConnect.Common.Enums.OpenAIEnums;

using OpenAIConnect.Services;

namespace OpenAIConnect.Tests.Services
{
    [TestClass]
    public class OpenAIClientTest
    {
        /// <summary>
        /// AppSettingsService
        /// </summary>
        private readonly AppSettingsService appSettingsService;

        private AppDbContext context;
        private IConfiguration configuration;

        private readonly HttpClient httpClient;
        private readonly IOpenAIClient openAIClient;

        public OpenAIClientTest()
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

            httpClient = new HttpClient();
            openAIClient = new OpenAIClient(appSettingsService, httpClient);
        }

        [TestMethod]
        public async Task SendRequestAsync()
        {
            // Arrange
            var prompts = new List<RequestMessage>
            {
                new RequestMessage
                {
                    Role = Role.System,
                    Content = "You are a helpful assistant."
                },
                new RequestMessage
                {
                    Role = Role.User,
                    Content = "What is the meaning of life?"
                }
            };

            // Act
            var response = await openAIClient.SendRequestAsync(prompts);

            Debug.WriteLine(response);

            // Assert
            Assert.IsNotNull(response);
        }
    }
}
