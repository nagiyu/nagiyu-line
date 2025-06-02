using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon;
using Amazon.CloudWatchLogs;
using Amazon.CloudWatchLogs.Model;
using Microsoft.Extensions.Configuration;

namespace CommonKit.Services
{
    public class LogService
    {
        private static readonly string logGroupName = "dev-nagiyu-line"; // ロググループ名
        private static readonly string logStreamName = "dev-nagiyu-line"; // ログストリーム名
        private static IAmazonCloudWatchLogs cloudWatchLogsClient;

        public LogService(IConfiguration configuration)
        {
            var region = configuration["AWS:Region"];
            var accessKey = configuration["AWS:AccessKey"];
            var secretKey = configuration["AWS:SecretKey"];

            cloudWatchLogsClient = new AmazonCloudWatchLogsClient(accessKey, secretKey, RegionEndpoint.GetBySystemName(region));
        }

        public async Task WriteLogAsync(string message)
        {
            var logEvent = new InputLogEvent
            {
                Message = message,
                Timestamp = DateTime.UtcNow
            };

            var putLogEventsRequest = new PutLogEventsRequest
            {
                LogGroupName = logGroupName,
                LogStreamName = logStreamName,
                LogEvents = new List<InputLogEvent> { logEvent }
            };

            await cloudWatchLogsClient.PutLogEventsAsync(putLogEventsRequest);
        }
    }
}
