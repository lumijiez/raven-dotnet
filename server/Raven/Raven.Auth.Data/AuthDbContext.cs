﻿using Raven.Auth.Data.Entities;

namespace Raven.Auth.Data;

using Microsoft.EntityFrameworkCore;

public class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasKey(u => u.Id); 

        modelBuilder.Entity<User>()
            .Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(50); 

        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100); 

        modelBuilder.Entity<User>()
            .Property(u => u.HashedPassword)
            .IsRequired()
            .HasMaxLength(256); 

        modelBuilder.Entity<User>()
            .Property(u => u.RegisterIp)
            .HasMaxLength(45); 
    }
}
