﻿using Marketer.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Marketer.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<UserModel> Users { get; set; }
    public DbSet<OrderModel> Orders { get; set; }
    public DbSet<CustomerModel> Customers { get; set; }
    public DbSet<ProductModel> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureUserModel(modelBuilder);
        ConfigureCustomerModel(modelBuilder);
        ConfigureProductModel(modelBuilder);
        ConfigureOrderModel(modelBuilder);
    }

    private static void ConfigureOrderModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderModel>()
            .HasKey(product => product.Id);
        modelBuilder.Entity<OrderModel>()
            .Property(user => user.CreationDate)
            .IsRequired();
        modelBuilder.Entity<OrderModel>()
            .Property(user => user.TotalPrice)
            .IsRequired();
        modelBuilder.Entity<OrderModel>()
            .HasMany(order => order.Products);
    }

    private static void ConfigureProductModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductModel>()
            .HasKey(product => product.Id);
        modelBuilder.Entity<ProductModel>()
            .Property(user => user.Name)
            .IsRequired()
            .HasMaxLength(100);
        modelBuilder.Entity<ProductModel>()
            .Property(user => user.Price)
            .IsRequired();
    }

    private static void ConfigureCustomerModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerModel>()
            .HasKey(customer => customer.Id);
        modelBuilder.Entity<CustomerModel>()
            .Property(user => user.FirstName)
            .IsRequired()
            .HasMaxLength(10);
        modelBuilder.Entity<CustomerModel>()
            .Property(user => user.LastName)
            .IsRequired()
            .HasMaxLength(10);
        modelBuilder.Entity<CustomerModel>()
            .Property(user => user.Age)
            .IsRequired();
        modelBuilder.Entity<CustomerModel>()
            .HasMany(x => x.Orders);
    }

    private static void ConfigureUserModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserModel>()
            .HasKey(user => user.Id);
        modelBuilder.Entity<UserModel>()
            .Property(user => user.UserName)
            .IsRequired()
            .HasMaxLength(10);
    }
}