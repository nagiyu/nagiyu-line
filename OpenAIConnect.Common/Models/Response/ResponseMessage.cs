using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static OpenAIConnect.Common.Enums.OpenAIEnums;

namespace OpenAIConnect.Common.Models.Response
{
    public class ResponseMessage
    {
        public Role Role { get; set; }

        public string Content { get; set; }

        public object Refusal { get; set; } // ここも内容が null っぽいから object に
    }
}
