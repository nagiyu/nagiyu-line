using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenAIConnect.Models.Request;

namespace OpenAIConnect.Interfaces
{
    public interface IOpenAIClient
    {
        Task<string> SendRequestAsync(List<RequestMessage> prompts);
    }
}
