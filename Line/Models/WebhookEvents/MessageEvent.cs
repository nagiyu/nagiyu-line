﻿using Line.Models.WebhookEvents.MessageObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace Line.Models.WebhookEvents
{
    /// <summary>
    /// メッセージイベント
    /// </summary>
    public class MessageEvent<T> : WebhookEventBase where T : MessageBase
    {
        /// <summary>
        /// 応答メッセージを送る際に使用する応答トークン
        /// </summary>
        public string ReplyToken { get; set; }

        /// <summary>
        /// メッセージの内容を含むオブジェクト
        /// </summary>
        public T Message { get; set; }
    }
}