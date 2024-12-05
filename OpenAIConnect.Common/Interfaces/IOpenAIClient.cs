using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenAIConnect.Common.Models.Request;

namespace OpenAIConnect.Common.Interfaces
{
    public interface IOpenAIClient
    {
        Task<string> SendRequestAsync(List<RequestMessage> prompts);
    }
}
