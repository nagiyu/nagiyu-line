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
        private static string logGroupName; // ロググループ名
        private static string baseLogStreamName; // 基本ログストリーム名
        private static IAmazonCloudWatchLogs cloudWatchLogsClient;

        public LogService(IConfiguration configuration)
        {
            var region = configuration["AWS:Region"];
            var accessKey = configuration["AWS:AccessKey"];
            var secretKey = configuration["AWS:SecretKey"];
            logGroupName = configuration["AWS:CloudWatch:LogGroupName"];
            baseLogStreamName = configuration["AWS:CloudWatch:LogStreamName"];

            // accessKeyとsecretKeyが空の場合は、本番環境（Lambda内やIAMロールが設定された環境）とみなし
            // 認証情報を明示的に指定せずにクライアントを初期化
            if (string.IsNullOrEmpty(accessKey) || string.IsNullOrEmpty(secretKey))
            {
                cloudWatchLogsClient = new AmazonCloudWatchLogsClient();
            }
            else
            {
                cloudWatchLogsClient = new AmazonCloudWatchLogsClient(accessKey, secretKey, RegionEndpoint.GetBySystemName(region));
            }
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
                LogStreamName = GetLogStreamNameWithDate(),
                LogEvents = new List<InputLogEvent> { logEvent }
            };

            await cloudWatchLogsClient.PutLogEventsAsync(putLogEventsRequest);
        }

        private string GetLogStreamNameWithDate()
        {
            var dateStr = DateTime.Now.ToString("yyyy-MM-dd");
            return $"{baseLogStreamName}-{dateStr}";
        }
    }
}
