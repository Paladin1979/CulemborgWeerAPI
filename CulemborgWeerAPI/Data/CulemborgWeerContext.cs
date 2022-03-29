using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CulemborgWeerAPI.Data
{
    public class CulemborgWeerContext : DbContext
    {
        private IConfiguration _config;

        public CulemborgWeerContext(IConfiguration config)
        {
            _config = config;
        }

        public DbSet<Entities.WeatherInformation> WeatherInformation { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite(_config.GetConnectionString("CulemborgWeerDb"));
        }
    }
}
