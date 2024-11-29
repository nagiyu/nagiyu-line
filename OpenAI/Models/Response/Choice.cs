using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAIConnect.Models.Response
{
    public class Choice
    {
        public int Index { get; set; }

        public ResponseMessage Message { get; set; }

        public string FinishReason { get; set; }

        public object Logprobs { get; set; } // ここは不明な構造なので object で
    }
}
