using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

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

        public async Task AddLineMessageAsync(LineMessage lineMessage)
        {
            await context.SaveAsync(lineMessage);
        }
    }
}
