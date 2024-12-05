using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OpenAIConnect.Common.Enums
{
    public class OpenAIEnums
    {
        [JsonConverter(typeof(StringEnumConverter), true)]
        public enum Role
        {
            System,
            Assistant,
            User
        }
    }
}
