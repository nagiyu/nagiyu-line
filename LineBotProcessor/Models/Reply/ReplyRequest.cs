using System.Collections.Generic;

namespace LineBotProcessor.Models.Reply
{
    public class ReplyRequest
    {
        public string ReplyToken { get; set; }
        public List<ReplyMessage> Messages { get; set; }
    }

    public class ReplyMessage
    {
        public string Type { get; set; }
        public string Text { get; set; }
    }
}
