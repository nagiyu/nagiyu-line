using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAIConnect.Models.Request
{
    public class OpenAIRequest
    {
        public string Model { get; set; }

        public List<RequestMessage> Messages { get; set; }
    }
}
