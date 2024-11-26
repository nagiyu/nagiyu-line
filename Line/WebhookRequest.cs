using System.Collections.Generic;

namespace Line
{
    public class WebhookRequest
    {
        public string Destination { get; set; }
        public List<Event> Events { get; set; }
    }

    public class Event
    {
        public string ReplyToken { get; set; }
        public string Type { get; set; }
        public string Mode { get; set; }
        public long Timestamp { get; set; } // Unix timestampなのでlong
        public Source Source { get; set; }
        public string WebhookEventId { get; set; }
        public DeliveryContext DeliveryContext { get; set; }
        public Message Message { get; set; }
    }

    public class Source
    {
        public string Type { get; set; }
        public string GroupId { get; set; }
        public string UserId { get; set; }
    }

    public class DeliveryContext
    {
        public bool IsRedelivery { get; set; }
    }

    public class Message
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string QuoteToken { get; set; }
        public string Text { get; set; }
        public List<Emoji> Emojis { get; set; }
        public Mention Mention { get; set; }
    }

    public class Emoji
    {
        public int Index { get; set; }
        public int Length { get; set; }
        public string ProductId { get; set; }
        public string EmojiId { get; set; }
    }

    public class Mention
    {
        public List<Mentionee> Mentionees { get; set; }
    }

    public class Mentionee
    {
        public int Index { get; set; }
        public int Length { get; set; }
        public string Type { get; set; }
        public string UserId { get; set; }
        public bool IsSelf { get; set; }
    }
}
