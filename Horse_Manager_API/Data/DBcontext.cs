using System;
using Microsoft.EntityFrameworkCore;
using Horse_Manager_API.Models;

namespace Horse_Manager_API.Data
{
    public class DBcontext : DbContext 
    {
        public DBcontext(DbContextOptions<DBcontext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Horse> Horses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Stable> Stables { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Subscription_Plan> Subscription_Plans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            optionsBuilder.UseSqlite("Data Source=Horse_Manager.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<User>().ToTable("Users").HasMany<Stable>(g => g.Own_Stables);
            modelBuilder.Entity<User>().HasMany<Worker>(f => f.Work);
            //modelBuilder.Entity<User>().HasMany<Stable>(f => f.Other_Stables);
            modelBuilder.Entity<Image>().ToTable("Images");
            modelBuilder.Entity<Horse>().ToTable("Horses").HasMany<Image>(g => g.Images);
            //modelBuilder.Entity<Category>().ToTable("Categories").HasMany<Horse>(g => g.Horses);
            modelBuilder.Entity<Stable>().ToTable("Stables").HasMany<Category>(g => g.Categories);
            modelBuilder.Entity<Stable>().HasMany<Image>(g => g.Images);
            modelBuilder.Entity<Stable>().HasMany<Horse>(g => g.Horses);
            modelBuilder.Entity<Stable>().HasMany<Worker>(g => g.Workers);
            //modelBuilder.Entity<Stable>().HasOne(g => g.Manager);
            modelBuilder.Entity<Worker>().ToTable("Workers").HasOne(g => g.User);
            modelBuilder.Entity<Worker>().HasOne(f => f.Stable);
            modelBuilder.Entity<Subscription_Plan>().ToTable("Subscription_Plans");
        }
    }
}
