using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using SettingsRepository;

namespace SettingsManager.Services
{
    public class AppSettingsService
    {
        private readonly AppDbContext _context;

        public AppSettingsService(AppDbContext context)
        {
            _context = context;
        }

        public string GetValueByKey(string key)
        {
            var setting = _context.AppSettings
                .AsNoTracking()
                .FirstOrDefault(s => s.Key == key && s.DeletedAt == null);

            return setting?.Value;
        }

        public T GetValueByKey<T>(string key)
        {
            var setting = _context.AppSettings
                .AsNoTracking()
                .FirstOrDefault(s => s.Key == key && s.DeletedAt == null);

            if (setting?.Value == null)
                return default!;

            // 型に応じて変換する
            return (T)Convert.ChangeType(setting.Value, typeof(T));
        }

        public async Task<string> GetValueByKeyAsync(string key)
        {
            var setting = await _context.AppSettings
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Key == key && s.DeletedAt == null);

            return setting?.Value;
        }

        public async Task<T> GetValueByKeyAsync<T>(string key)
        {
            var setting = await _context.AppSettings
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Key == key && s.DeletedAt == null);

            if (setting?.Value == null)
                return default!;

            // 型に応じて変換する
            return (T)Convert.ChangeType(setting.Value, typeof(T));
        }
    }
}
