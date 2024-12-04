using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static OpenAIConnect.Common.Enums.OpenAIEnums;

namespace OpenAIConnect.Common.Models.Request
{
    public class RequestMessage
    {
        public Role Role { get; set; }

        public string Content { get; set; }
    }
}
