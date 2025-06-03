using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OpenAIConnect.Common.Interfaces;
using OpenAIConnect.Common.Models.Request;
using OpenAIConnect.Services;
using static OpenAIConnect.Common.Enums.OpenAIEnums;

namespace OpenAIConnect.Tests.Services
{
    [TestClass]
    public class OpenAIClientTest
    {
        private readonly HttpClient httpClient;
        private readonly IOpenAIClient openAIClient;

        public OpenAIClientTest()
        {
            var basePath = Directory.GetCurrentDirectory();
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();

            httpClient = new HttpClient();
            openAIClient = new OpenAIClient(httpClient, configuration);
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
