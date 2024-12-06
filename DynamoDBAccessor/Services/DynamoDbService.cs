using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;

using CommonKit.Utilities;

using SettingsManager.Services;

using DynamoDBAccessor.Interfaces;
using DynamoDBAccessor.Models;

namespace DynamoDBAccessor.Services
{
    public class DynamoDbService : IDynamoDbService
    {
        /// <summary>
        /// AppSettingsService
        /// </summary>
        private readonly AppSettingsService appSettingsService;

        private readonly AmazonDynamoDBClient client;
        private readonly DynamoDBContext context;

        public DynamoDbService(AppSettingsService appSettingsService)
        {
            this.appSettingsService = appSettingsService;

            var region = appSettingsService.GetValueByKey("AWS:Region");
            var accessKey = appSettingsService.GetValueByKey("AWS:AccessKey");
            var secretKey = appSettingsService.GetValueByKey("AWS:SecretKey");

            client = new AmazonDynamoDBClient(accessKey, secretKey, RegionEndpoint.GetBySystemName(region));

            context = new DynamoDBContext(client);
        }

        public async Task<List<LineMessage>> GetLineMessageByUserIDAsync(string userId, List<string> excludeWords = null)
        {
            // excludeWords が null の場合は空リストを設定
            excludeWords ??= new List<string>();

            var oneHourAgo = DateTimeOffset.UtcNow.AddHours(-1).ToUnixTimeSeconds() * 1000; // 1時間前のUNIXタイム取得

            var queryRequest = new QueryRequest
            {
                TableName = await appSettingsService.GetValueByKeyAsync("AWS:DynamoDB:TableName"), // テーブル名
                IndexName = await appSettingsService.GetValueByKeyAsync("AWS:DynamoDB:IndexName"), // GSIの名前
                KeyConditionExpression = "UserId = :userId AND EventTimestamp >= :oneHourAgo",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    { ":userId", new AttributeValue { S = userId } },
                    { ":oneHourAgo", new AttributeValue { N = oneHourAgo.ToString() } }
                },
                ScanIndexForward = false
            };

            // クエリ実行
            var response = await client.QueryAsync(queryRequest);

            var resultList = new List<LineMessage>();
            var maxResults = await appSettingsService.GetValueByKeyAsync<int>("AWS:DynamoDB:MaxMessages"); // 最大取得件数の設定

            foreach (var item in response.Items)
            {
                var messageText = item["MessageText"].S;

                // 除外文言が指定されていて、含まれている場合は終了
                if (excludeWords.Contains(messageText))
                {
                    break;
                }

                resultList.Insert(0, new LineMessage
                {
                    Id = Guid.Parse(item["Id"].S),
                    UserId = item["UserId"].S,
                    GroupId = null,
                    RoomId = null,
                    EventTimestamp = long.Parse(item["EventTimestamp"].N),
                    EventType = null,
                    MessageId = null,
                    MessageText = messageText,
                    ReplyText = item["ReplyText"].S
                });

                // 最大件数に到達したら終了
                if (resultList.Count >= maxResults)
                {
                    break;
                }
            }

            return resultList;
        }

        public async Task<int> GetTodayLineMessageCountAsync(string userId, List<string> excludeWords = null)
        {
            // excludeWords が null の場合は空リストを設定
            excludeWords ??= new List<string>();

            // 今日の日付の0時を取得（UTCで）
            var startOfToday = DateTime.UtcNow.Date;
            var startOfTomorrow = startOfToday.AddDays(1);

            // ベースクエリ作成
            var queryRequest = new QueryRequest
            {
                TableName = await appSettingsService.GetValueByKeyAsync("AWS:DynamoDB:TableName"), // テーブル名
                IndexName = await appSettingsService.GetValueByKeyAsync("AWS:DynamoDB:IndexName"), // GSIの名前
                KeyConditionExpression = "UserId = :userId AND EventTimestamp BETWEEN :startOfToday AND :startOfTomorrow",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    { ":userId", new AttributeValue { S = userId } },
                    { ":startOfToday", new AttributeValue { N = (((DateTimeOffset)startOfToday).ToUnixTimeSeconds() * 1000).ToString() } },
                    { ":startOfTomorrow", new AttributeValue { N = (((DateTimeOffset)startOfTomorrow).ToUnixTimeSeconds() * 1000).ToString() } }
                }
            };

            // 除外文言があればFilterExpressionを追加
            if (excludeWords.Count > 0)
            {
                var filterConditions = new string[excludeWords.Count];

                for (int i = 0; i < excludeWords.Count; i++)
                {
                    var placeholder = $":excludeWord{i}";
                    filterConditions[i] = $"MessageText <> {placeholder}";
                    queryRequest.ExpressionAttributeValues.Add(placeholder, new AttributeValue { S = excludeWords[i] });
                }

                queryRequest.FilterExpression = string.Join(" AND ", filterConditions);
            }

            // クエリ実行
            var response = await client.QueryAsync(queryRequest);

            // メッセージ数を返す
            return response.Count;
        }

        public async Task AddLineMessageAsync(LineMessage lineMessage)
        {
            var tableName = await appSettingsService.GetValueByKeyAsync("AWS:DynamoDB:TableName");

            var config = new DynamoDBOperationConfig
            {
                OverrideTableName = tableName
            };

            await context.SaveAsync(lineMessage, config);
        }
    }
}
