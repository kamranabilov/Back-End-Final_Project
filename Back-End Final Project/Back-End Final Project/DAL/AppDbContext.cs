using Back_End_Final_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Back_End_Final_Project.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Clothes> Clothes { get; set; }
        public DbSet<ClothesImage> ClothesImages { get; set; }
        public DbSet<ClothesInformation> ClothesInformations { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ContactUs> ContactUss { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var item in modelBuilder.Model.GetEntityTypes()
               .SelectMany(e => e.GetProperties()
               .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
               )
            {
                item.SetColumnType("decimal(6,2)");
                //item.SetDefaultValue(20.5m);
            }
            modelBuilder.Entity<Setting>()
                        .HasIndex(s => s.Key)
                        .IsUnique();

            modelBuilder.Entity<Category>()
               .HasIndex(c => c.Name)
               .IsUnique();
            base.OnModelCreating(modelBuilder);
        }
    }
}
