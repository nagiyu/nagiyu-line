using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using SettingsRepository.Models;

namespace SettingsRepository
{
    public class AppDbContext : DbContext
    {
        public DbSet<AppSetting> AppSettings { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppSetting>()
                .HasKey(a => a.Key); // KeyをPrimaryKeyに指定
        }
    }
}
