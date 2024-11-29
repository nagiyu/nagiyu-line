﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Common.Utilities;
using Microsoft.Extensions.Configuration;

using OpenAIConnect.Interfaces;
using OpenAIConnect.Services;

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

            AppSettings.Initialize(builder.Build());

            httpClient = new HttpClient();
            openAIClient = new OpenAIClient(httpClient);
        }

        [TestMethod]
        public async Task SendRequestAsync()
        {
            // Arrange
            var prompt = "What is the meaning of life?";

            // Act
            var response = await openAIClient.SendRequestAsync(prompt);

            Debug.WriteLine(response);

            // Assert
            Assert.IsNotNull(response);
        }
    }
}