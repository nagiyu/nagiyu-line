using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineBridge.Models.MessageObjects
{
    /// <summary>
    /// メッセージ共通プロパティ
    /// </summary>
    public class ObjectBase
    {
        /// <summary>
        /// クイックリプライ (WIP)
        /// </summary>
        public object QuickReply { get; set; }

        /// <summary>
        /// アイコンと表示名のカスタマイズ (WIP)
        /// </summary>
        public object IconNicknameSwitch { get; set; }
    }
}
