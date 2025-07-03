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
        public async Task GetSecretAsync_ReturnsSecret()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables();
            var configuration = builder.Build();
            var service = new SecretsManagerService(configuration);
            // テスト用のシークレット名を指定してください
            var secretName = "dummy-secret-name";
            try
            {
                var secret = await service.GetSecretAsync(secretName);
                Assert.IsNotNull(secret);
            }
            catch (Amazon.SecretsManager.Model.ResourceNotFoundException)
            {
                Assert.Inconclusive("Secret not found. Set up a test secret in AWS Secrets Manager.");
            }
        }
    }
}
