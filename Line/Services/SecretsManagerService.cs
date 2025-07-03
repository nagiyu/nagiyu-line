using System;
using System.Threading.Tasks;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.Extensions.Configuration;

namespace Line.Services
{
    public class SecretsManagerService
    {
        private readonly IAmazonSecretsManager _secretsManager;
        private readonly string _region;

        public SecretsManagerService(IConfiguration configuration)
        {
            _region = configuration["AWS:Region"];
            _secretsManager = new AmazonSecretsManagerClient(Amazon.RegionEndpoint.GetBySystemName(_region));
        }

        public async Task<string> GetSecretAsync(string secretName)
        {
            var request = new GetSecretValueRequest
            {
                SecretId = secretName
            };
            var response = await _secretsManager.GetSecretValueAsync(request);
            return response.SecretString;
        }
    }
}
