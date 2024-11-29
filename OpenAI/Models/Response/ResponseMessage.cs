using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAIConnect.Models.Response
{
    public class ResponseMessage
    {
        public string Role { get; set; }

        public string Content { get; set; }

        public object Refusal { get; set; } // ここも内容が null っぽいから object に
    }
}
