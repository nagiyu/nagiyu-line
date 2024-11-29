using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAIConnect.Interfaces
{
    public interface IOpenAIClient
    {
        Task<string> SendRequestAsync(string prompt);
    }
}
