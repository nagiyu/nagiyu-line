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

using DynamoDBAccessor.Interfaces;
using DynamoDBAccessor.Models;

namespace DynamoDBAccessor.Services
{
    public class DynamoDbService : IDynamoDbService
    {
        private readonly AmazonDynamoDBClient client;
        private readonly DynamoDBContext context;

        public DynamoDbService()
        {
            var region = AppSettings.GetSetting("AWS:Region");
            var accessKey = AppSettings.GetSetting("AWS:AccessKey");
            var secretKey = AppSettings.GetSetting("AWS:SecretKey");

            client = new AmazonDynamoDBClient(accessKey, secretKey, RegionEndpoint.GetBySystemName(region));

            context = new DynamoDBContext(client);
        }

        public async Task<List<LineMessage>> GetLineMessageByUserIDAsync(string userId, List<string> excludeWords = null)
        {
            // excludeWords が null の場合は空リストを設定
            excludeWords ??= new List<string>();

            var oneHourAgo = DateTimeOffset.UtcNow.AddHours(-1).ToUnixTimeSeconds(); // 1時間前のUNIXタイム取得

            var queryRequest = new QueryRequest
            {
                TableName = AppSettings.GetSetting("AWS:DynamoDB:TableName"), // テーブル名
                IndexName = AppSettings.GetSetting("AWS:DynamoDB:IndexName"), // GSIの名前
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
            var maxResults = AppSettings.GetSetting<int>("AWS:DynamoDB:MaxMessages"); // 最大取得件数の設定

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
                TableName = AppSettings.GetSetting("AWS:DynamoDB:TableName"), // テーブル名
                IndexName = AppSettings.GetSetting("AWS:DynamoDB:IndexName"), // GSIの名前
                KeyConditionExpression = "UserId = :userId AND EventTimestamp BETWEEN :startOfToday AND :startOfTomorrow",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    { ":userId", new AttributeValue { S = userId } },
                    { ":startOfToday", new AttributeValue { N = ((DateTimeOffset)startOfToday).ToUnixTimeSeconds().ToString() } },
                    { ":startOfTomorrow", new AttributeValue { N = ((DateTimeOffset)startOfTomorrow).ToUnixTimeSeconds().ToString() } }
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
            var tableName = AppSettings.GetSetting("AWS:DynamoDB:TableName");

            var config = new DynamoDBOperationConfig
            {
                OverrideTableName = tableName
            };

            await context.SaveAsync(lineMessage, config);
        }
    }
}
