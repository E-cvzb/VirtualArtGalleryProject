using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGalleryProject.Data.Entities;

namespace VirtualArtGalleryProject.Data.Context
{
    public class VirtualArtGalleryProjectDbContext:DbContext
    {
        public VirtualArtGalleryProjectDbContext(DbContextOptions<VirtualArtGalleryProjectDbContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ArtworkConfiguration());
            modelBuilder.ApplyConfiguration(new ExhibitionArtworkConfiguration());
            modelBuilder.ApplyConfiguration(new ExhibitionConfiguration());
            modelBuilder.ApplyConfiguration(new PurchasConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.Entity<SettingEntity>().HasData(
                new SettingEntity
                {
                    Id= 1,
                    MaintenenceMode=false,
                });

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<UserEntity> User { get; set; }
        public DbSet<ExhibitionArtworkEntity> ExhibitionArtwork { get; set; }
        public DbSet<ExhibitionEntity> Exhibition { get; set; }
        public DbSet<ArtworkEntity> Artwork { get; set; }
        public DbSet<PurchaseEntity> Purchase { get; set; }
        public DbSet<SettingEntity> Setting { get; set; }

    }
}
