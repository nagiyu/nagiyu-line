using Line.Models.WebhookEvents.MessageObjects;

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Line
{
    public class MessageConverter : JsonConverter<MessageBase>
    {
        public override MessageBase ReadJson(JsonReader reader, Type objectType, MessageBase existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            // JSONを中身ごと読み込む
            var jsonObject = JObject.Load(reader);

            // "type"フィールドを見て適切なクラスにデシリアライズ
            var type = jsonObject["type"]?.ToString();
            switch (type)
            {
                case "text":
                    return jsonObject.ToObject<TextMessage>(serializer);
                case "image":
                    return jsonObject.ToObject<ImageMessage>(serializer);
                default:
                    throw new NotSupportedException($"Unsupported message type: {type}");
            }
        }

        public override void WriteJson(JsonWriter writer, MessageBase value, JsonSerializer serializer)
        {
            // 書き出しも対応する場合はこっち（今回はオプションなので空でOK）
            serializer.Serialize(writer, value);
        }
    }
}
