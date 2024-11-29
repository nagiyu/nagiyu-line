using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Common.Utilities
{
    /// <summary>
    /// JSON シリアライズやキーのキャメルケース変換を行う
    /// </summary>
    public static class JsonHelper
    {
        public static T Deserialize<T>(string json)
        {
            // Snake CaseやCamel Caseのキーを自動変換
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy() // Camel Case対応（例: name, age）
                }
            };

            return JsonConvert.DeserializeObject<T>(json, settings);
        }

        public static string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
    }
}
