﻿using IsThisGeekAlive.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsThisGeekAlive.Data
{
    public class GeekDbContext : DbContext
    {
        public GeekDbContext()
            : base()
        {
        }

        public GeekDbContext(DbContextOptions<GeekDbContext> options)
            : base(options)
        {
        }

        public DbSet<Geek> Geeks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingGeek(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        void OnModelCreatingGeek(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Geek>().HasKey(x => x.GeekId);
            modelBuilder.Entity<Geek>().HasIndex(x => x.UsernameLower).HasName("Geek_UsernameLower").IsUnique();

            modelBuilder.Entity<Geek>().Property(x => x.GeekId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Geek>().Property(x => x.Username).IsRequired().HasMaxLength(500);        
            modelBuilder.Entity<Geek>().Property(x => x.UsernameLower).IsRequired().HasMaxLength(500);
            modelBuilder.Entity<Geek>().Property(x => x.LoginCode).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Geek>().Property(x => x.NotAliveWarningWindow).IsRequired();
            modelBuilder.Entity<Geek>().Property(x => x.NotAliveDangerWindow).IsRequired();
            modelBuilder.Entity<Geek>().Property(x => x.LastActivityLocalTime).HasColumnType("TIMESTAMP").IsRequired();
            modelBuilder.Entity<Geek>().Property(x => x.LastActivityLocalTimeUtcOffset).IsRequired();
            modelBuilder.Entity<Geek>().Property(x => x.LastActivityServerTime).HasColumnType("TIMESTAMP").IsRequired();
            modelBuilder.Entity<Geek>().Property(x => x.LastActivityServerTimeUtcOffset).IsRequired();
        }
    }
}
