using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmFresh.Model
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-AUFLTT7F\SQLEXPRESS;Database=master;Initial Catalog=StoreDB;Integrated Security=SSPI;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product_Category>()
                .HasKey(pc => new { pc.ProductID, pc.CategoryID });

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Product_Categories)
                .WithOne(p => p.Product)
                .HasForeignKey(p => p.ProductID);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Product_Categories)
                .WithOne(c => c.Category)
                .HasForeignKey(c => c.CategoryID);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Product_Category> ProductCategories { get; set; }
        public DbSet<Category> Category { get; set; }

    }
}
