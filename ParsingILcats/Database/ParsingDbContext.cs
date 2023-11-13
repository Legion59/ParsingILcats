using AngleSharp;
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

        public ParsingDbContext() : base()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MarketModel>(market =>
            {
                market.ToTable("Markets");
                market.HasKey(x => x.Id);
                market.Property(x => x.Id).ValueGeneratedOnAdd().IsRequired();
                market.Ignore(x => x.LinkCarModel);
            });

            modelBuilder.Entity<CarModel>(car =>
            {
                car.ToTable("Models");
                car.HasKey(x => x.Id);
                car.Property(x => x.Id).ValueGeneratedOnAdd().IsRequired();
                car.Ignore(x => x.LinkConfiguration);

                car.HasOne(x => x.Market)
                   .WithMany(x => x.Cars)
                   .HasForeignKey(x => x.MarketId)
                   .IsRequired();
            });

            modelBuilder.Entity<ConfigurationModel>(configuration =>
            {
                configuration.ToTable("Configurations");
                configuration.HasKey(x => x.Id);
                configuration.Property(x => x.Id).ValueGeneratedOnAdd().IsRequired();
                configuration.Ignore(x => x.LinkToGroupPage);

                configuration.HasOne(x => x.Car)
                             .WithMany(x => x.Configurations)
                             .HasForeignKey(x => x.CarId)
                             .IsRequired();
            });

            modelBuilder.Entity<GroupModel>(group =>
            {
                group.ToTable("Groups");
                group.HasKey(x => x.Id);
                group.Property(x => x.Id).ValueGeneratedOnAdd().IsRequired();
                group.Ignore(x => x.LinkSubGroup);

                group.HasOne(x => x.Configuration)
                             .WithMany(x => x.Groups)
                             .HasForeignKey(x => x.ConfigurationId)
                             .IsRequired();
            });

            modelBuilder.Entity<SubGroupModel>(subGroup =>
            {
                subGroup.ToTable("SubGroups");
                subGroup.HasKey(x => x.Id);
                subGroup.Property(x => x.Id).ValueGeneratedOnAdd().IsRequired();
                subGroup.Ignore(x => x.LinkToParts);

                subGroup.HasOne(x => x.Group)
                             .WithMany(x => x.SubGroups)
                             .HasForeignKey(x => x.GroupId)
                             .IsRequired();
            });

            modelBuilder.Entity<PartsModel>(parts =>
            {
                parts.ToTable("Parts");
                parts.HasKey(x => x.Id);
                parts.Property(x => x.Code).ValueGeneratedOnAdd().IsRequired();

                parts.HasOne(x => x.SubGroup)
                             .WithMany(x => x.Parts)
                             .HasForeignKey(x => x.SubGroupId)
                             .IsRequired();
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _ = optionsBuilder.UseSqlServer("Server=DESKTOP-FNOV9O5\\SQLEXPRESS;Database=ILcats;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
