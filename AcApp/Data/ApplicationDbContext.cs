﻿using AcApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AcApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Replace with your actual database connection string
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False",
                    b => b.MigrationsAssembly("AcApp"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships, keys, and constraints here
            modelBuilder.Entity<Product>()
        .ToTable("AcApp");
            // Example: Configure the OrderItems table relationships
            modelBuilder.Entity<OrderItems>()
                .HasOne<Order>() // Specify the Order entity
                .WithMany(o => o.OrderItems) // An order can have many items
                .HasForeignKey(oi => oi.OrderId) // Foreign key on OrderItems
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete if the Order is deleted

            modelBuilder.Entity<OrderItems>()
                .HasOne<Product>() // Specify the Product entity
                .WithMany() // A product can belong to many orders
                .HasForeignKey(oi => oi.Id) // Foreign key on OrderItems
                .OnDelete(DeleteBehavior.Restrict); // Restrict deletion of Product if referenced by OrderItems

            // Additional configuration for unique constraints, indexes, etc.
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Email)
                .IsUnique(); // Ensure emails are unique in the Customers table
        }

        // Define DbSet properties for each entity in the application
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
    }
}