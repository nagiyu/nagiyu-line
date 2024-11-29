using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

namespace Common.Utilities
{
    public static class AppSettings
    {
        private static IConfiguration _configuration;

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="configuration">Configuration</param>
        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 値を取得する
        /// </summary>
        /// <param name="key">キー</param>
        /// <returns>値</returns>
        public static string GetSetting(string key)
        {
            if (_configuration == null)
            {
                throw new InvalidOperationException("AppSettings is not initialized. Call Initialize() first.");
            }

            return _configuration[key];
        }
    }
}
