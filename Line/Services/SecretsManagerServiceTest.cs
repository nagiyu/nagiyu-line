using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Configuration;
using Line.Services;

namespace Line.Tests
{
    [TestClass]
    public class SecretsManagerServiceTest
    {
        [TestMethod]
        public async Task GetSecretValueByKeyAsync_ReturnsSecretValue()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables();
            var configuration = builder.Build();
            var service = new SecretsManagerService(configuration);
            // テスト用のシークレット名とキー名を指定してください
            var secretName = "dummy-secret-name";
            var key = "dummy-key";
            try
            {
                var value = await service.GetSecretValueByKeyAsync(secretName, key);
                Assert.IsNotNull(value);
            }
            catch (Amazon.SecretsManager.Model.ResourceNotFoundException)
            {
                Assert.Inconclusive("Secret not found. Set up a test secret in AWS Secrets Manager.");
            }
        }
    }
}
