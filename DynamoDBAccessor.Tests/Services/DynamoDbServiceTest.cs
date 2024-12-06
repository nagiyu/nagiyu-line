using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using CommonKit.Utilities;

using SettingsManager.Services;
using SettingsRepository;

using DynamoDBAccessor.Interfaces;
using DynamoDBAccessor.Models;
using DynamoDBAccessor.Services;

namespace DynamoDBAccessor.Tests.Services
{
    [TestClass]
    public class DynamoDbServiceTest
    {
        /// <summary>
        /// AppSettingsService
        /// </summary>
        private readonly AppSettingsService appSettingsService;

        private AppDbContext context;
        private IConfiguration configuration;

        private readonly IDynamoDbService dynamoDbService;

        public DynamoDbServiceTest()
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

            dynamoDbService = new DynamoDbService(appSettingsService);
        }

        [TestMethod]
        public async Task AddLineMessageAsync()
        {
            // Arrange
            var lineMessage = new LineMessage
            {
                Id = Guid.NewGuid(),
                UserId = "test-user-id",
                GroupId = "test-group-id",
                RoomId = "test-room-id",
                EventTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                EventType = "message",
                MessageId = "test-message-id",
                MessageText = "test-message-text",
                ReplyText = "test-reply-text"
            };

            // Act
            await dynamoDbService.AddLineMessageAsync(lineMessage);

            // Assert
            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task GetTodayLineMessageCountAsync_NoFilters()
        {
            // Arrange
            var userId = "test-user-id";

            // Act
            var result = await dynamoDbService.GetTodayLineMessageCountAsync(userId);

            Debug.WriteLine(result);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetTodayLineMessageCountAsync_EmptyFilters()
        {
            // Arrange
            var userId = "test-user-id";

            // Act
            var result = await dynamoDbService.GetTodayLineMessageCountAsync(userId, new List<string> { });

            Debug.WriteLine(result);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetTodayLineMessageCountAsync_WithResetFilter()
        {
            // Arrange
            var userId = "test-user-id";

            // Act
            var result = await dynamoDbService.GetTodayLineMessageCountAsync(userId, new List<string> { "リセット" });

            Debug.WriteLine(result);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetLineMessageByUserIDAsync_NoFilters()
        {
            // Arrange
            var userId = "test-user-id";

            // Act
            var result = await dynamoDbService.GetLineMessageByUserIDAsync(userId);

            foreach (var item in result)
            {
                Debug.WriteLine($"{item.UserId}, {item.EventTimestamp}, {item.MessageText}, {item.ReplyText}");
            }

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetLineMessageByUserIDAsync_EmptyFilters()
        {
            // Arrange
            var userId = "test-user-id";

            // Act
            var result = await dynamoDbService.GetLineMessageByUserIDAsync(userId, new List<string> { });

            foreach (var item in result)
            {
                Debug.WriteLine($"{item.UserId}, {item.EventTimestamp}, {item.MessageText}, {item.ReplyText}");
            }

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetLineMessageByUserIDAsync_WithResetFilter()
        {
            // Arrange
            var userId = "test-user-id";

            // Act
            var result = await dynamoDbService.GetLineMessageByUserIDAsync(userId, new List<string> { "リセット" });

            foreach (var item in result)
            {
                Debug.WriteLine($"{item.UserId}, {item.EventTimestamp}, {item.MessageText}, {item.ReplyText}");
            }

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
