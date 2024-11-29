using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAIConnect.Models.Response
{
    public class TokenDetails
    {
        public int CachedTokens { get; set; }

        public int AudioTokens { get; set; }

        public int AcceptedPredictionTokens { get; set; }

        public int RejectedPredictionTokens { get; set; }
    }
}
