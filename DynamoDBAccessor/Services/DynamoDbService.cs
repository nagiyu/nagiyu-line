using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

using Common.Utilities;

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

        public async Task<List<LineMessage>> GetLineMessageByUserIDAsync(string userId)
        {
            var queryRequest = new QueryRequest
            {
                TableName = "LineMessages", // テーブル名
                IndexName = "UserId-EventTimestamp-index",   // GSIの名前
                KeyConditionExpression = "UserId = :userId",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    { ":userId", new AttributeValue { S = userId } }
                },
                ScanIndexForward = false
            };

            var response = await client.QueryAsync(queryRequest);

            var resultList = new List<LineMessage>();
            int maxResults = 20; // 最大取得件数の設定

            foreach (var item in response.Items)
            {
                var messageText = item["MessageText"].S;

                // "リセット" が見つかったら処理終了
                if (messageText == "リセット")
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
                    MessageText = item["MessageText"].S,
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

        public async Task AddLineMessageAsync(LineMessage lineMessage)
        {
            await context.SaveAsync(lineMessage);
        }
    }
}
