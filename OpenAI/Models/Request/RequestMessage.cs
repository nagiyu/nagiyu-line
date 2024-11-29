using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAIConnect.Models.Request
{
    public class RequestMessage
    {
        public string Role { get; set; }

        public string Content { get; set; }
    }
}
