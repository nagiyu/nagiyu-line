using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using Common.Utilities;

using DynamoDBAccessor.Interfaces;
using DynamoDBAccessor.Models;
using DynamoDBAccessor.Services;

namespace DynamoDBAccessor.Tests.Services
{
    [TestClass]
    public class DynamoDbServiceTest
    {
        private readonly IDynamoDbService dynamoDbService;

        public DynamoDbServiceTest()
        {
            var basePath = Directory.GetCurrentDirectory();
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            AppSettings.Initialize(builder.Build());

            dynamoDbService = new DynamoDbService();
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
        public async Task GetTodayLineMessageCountAsync()
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
        public async Task GetLineMessageByUserIDAsync()
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
    }
}
