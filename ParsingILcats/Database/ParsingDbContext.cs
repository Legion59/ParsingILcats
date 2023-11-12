using Microsoft.EntityFrameworkCore;
using ParsingILcats.Models;

namespace ParsingILcats.Database
{
    public class ParsingDbContext : DbContext
    {
        public DbSet<MarketModel> MarketSet { get; set; }
        public DbSet<CarModel> CarSet { get; set; }
        public DbSet<ConfigurationModel> ConfigurationSet { get; set; }
        public DbSet<GroupModel> GroupSet { get; set; }
        public DbSet<SubGroupModel > SubGroupSet { get; set; }
        public DbSet<PartsModel> PartsSet { get; set; }

        protected ParsingDbContext() : base()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _ = optionsBuilder.UseSqlServer("Server=DESKTOP-FNOV9O5\\SQLEXPRESS;Database=ILcats;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
