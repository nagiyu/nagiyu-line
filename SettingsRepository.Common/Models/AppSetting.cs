using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsRepository.Models
{
    public class AppSetting
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Memo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; } // Nullable型でソフトデリートに対応
    }
}
