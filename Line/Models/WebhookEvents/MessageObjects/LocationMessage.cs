using System.ComponentModel.DataAnnotations;

namespace Line.Models.WebhookEvents.MessageObjects
{
    public class LocationMessage : MessageBase
    {
        /// <summary>
        /// メッセージID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// タイトル
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 住所
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 緯度
        /// </summary>
        public decimal Latitude { get; set; }

        /// <summary>
        /// 経度
        /// </summary>
        public decimal Longitude { get; set; }
    }
}
