﻿using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Core.Security.Entities;

namespace ECommerce.Persistence.Contexts;

public class BaseDbContext : DbContext
{

    public BaseDbContext(DbContextOptions<BaseDbContext> opt): base(opt)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        modelBuilder.Entity<User>()
            .ToTable("AppUsers") // Tek tablo kullanımı
            .HasDiscriminator<string>("Discriminator") // Discriminator sütunu ekle
            .HasValue<User>("User")
            .HasValue<AppUser>("AppUser");
        
        
        modelBuilder.Entity<UserOperationClaim>()
            .HasOne(uoc => uoc.User)
            .WithMany(u => u.UserOperationClaims)
            .HasForeignKey(uoc => uoc.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<User>()
            .HasQueryFilter(u => !u.DeletedDate.HasValue);

    }

   public DbSet<Category> Categories { get; set; }
   /*public DbSet<Product> Products { get; set; }
   public DbSet<ProductImage> ProductImages { get; set; }
   public DbSet<SubCategory> SubCategories { get; set; }*/
   public DbSet<AppUser> AppUsers { get; set; }
   /*public DbSet<Order> Orders { get; set; }
   public DbSet<OrderItem> OrderItems { get; set; }
   public DbSet<Tag> Tags { get; set; }
   public DbSet<ProductTag> ProductTags { get; set; }*/
   
   
   public DbSet<OperationClaim> OperationClaims { get; set; }
   public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
   
}