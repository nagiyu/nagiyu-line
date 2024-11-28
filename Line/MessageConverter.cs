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
                    // 個別のシリアライザーを使って無限ループを回避
                    return jsonObject.ToObject<TextMessage>(CreateNoConverterSerializer(serializer));
                case "image":
                    return jsonObject.ToObject<ImageMessage>(CreateNoConverterSerializer(serializer));
                default:
                    throw new NotSupportedException($"Unsupported message type: {type}");
            }
        }

        public override void WriteJson(JsonWriter writer, MessageBase value, JsonSerializer serializer)
        {
            // 書き出し処理（今回は空でもOK）
            serializer.Serialize(writer, value);
        }

        private JsonSerializer CreateNoConverterSerializer(JsonSerializer baseSerializer)
        {
            // オリジナルのシリアライザー設定を複製して、カスタムコンバーターを除外したインスタンスを作成
            var newSerializer = new JsonSerializer
            {
                ContractResolver = baseSerializer.ContractResolver,
                NullValueHandling = baseSerializer.NullValueHandling,
                DefaultValueHandling = baseSerializer.DefaultValueHandling,
                ObjectCreationHandling = baseSerializer.ObjectCreationHandling,
                MissingMemberHandling = baseSerializer.MissingMemberHandling,
                ReferenceLoopHandling = baseSerializer.ReferenceLoopHandling,
                PreserveReferencesHandling = baseSerializer.PreserveReferencesHandling,
                TypeNameHandling = baseSerializer.TypeNameHandling,
                MetadataPropertyHandling = baseSerializer.MetadataPropertyHandling,
                Formatting = baseSerializer.Formatting
            };

            // カスタムコンバーターを取り除く
            newSerializer.Converters.Clear();

            return newSerializer;
        }
    }

}
