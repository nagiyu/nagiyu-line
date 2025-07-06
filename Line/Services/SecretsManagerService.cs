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

        public async Task<string> GetSecretValueByKeyAsync(string secretName, string key)
        {
            var request = new GetSecretValueRequest
            {
                SecretId = secretName
            };
            var response = await _secretsManager.GetSecretValueAsync(request);
            var secretString = response.SecretString;
            if (string.IsNullOrEmpty(secretString))
                return null;
            try
            {
                var dict = System.Text.Json.JsonSerializer.Deserialize<System.Collections.Generic.Dictionary<string, string>>(secretString);
                if (dict != null && dict.ContainsKey(key))
                    return dict[key];
                return null;
            }
            catch (System.Text.Json.JsonException)
            {
                return null;
            }
        }
    }
}
