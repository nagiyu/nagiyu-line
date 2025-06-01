using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon;
using Amazon.CloudWatchLogs;
using Amazon.CloudWatchLogs.Model;
using SettingsManager.Services;

namespace CommonKit.Services
{
    public class LogService
    {
        private static readonly string logGroupName = "dev-nagiyu-line"; // ロググループ名
        private static readonly string logStreamName = "dev-nagiyu-line"; // ログストリーム名
        private static IAmazonCloudWatchLogs cloudWatchLogsClient;

        public LogService(AppSettingsService appSettingsService)
        {
            var region = appSettingsService.GetValueByKey("AWS:Region");
            var accessKey = appSettingsService.GetValueByKey("AWS:AccessKey");
            var secretKey = appSettingsService.GetValueByKey("AWS:SecretKey");

            cloudWatchLogsClient = new AmazonCloudWatchLogsClient(accessKey, secretKey, RegionEndpoint.GetBySystemName(region)); // デフォルトのクレデンシャルを使用
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
