﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StudentPortal.Models.Models
{
    public partial class stdportalContext : DbContext
    {
        public stdportalContext()
        {
        }

        public stdportalContext(DbContextOptions<stdportalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DeviceTokens> DeviceTokens { get; set; }
        public virtual DbSet<ForgotPassword> ForgotPassword { get; set; }
        public virtual DbSet<Logins> Logins { get; set; }
        public virtual DbSet<Lookup> Lookup { get; set; }
        public virtual DbSet<Notifications> Notifications { get; set; }
        public virtual DbSet<TimeTable> TimeTable { get; set; }
        public virtual DbSet<UserProfile> UserProfile { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=67.225.176.164; Database=stdportal; User Id=portal; password=Portal@123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:DefaultSchema", "portal");

            modelBuilder.Entity<DeviceTokens>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("DeviceTokens", "dbo");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.DeviceToken).IsRequired();

                entity.HasOne(d => d.User)
                    .WithOne(p => p.DeviceTokens)
                    .HasForeignKey<DeviceTokens>(d => d.UserId)
                    .HasConstraintName("FK_DeviceTokens_UserProfile");
            });

            modelBuilder.Entity<ForgotPassword>(entity =>
            {
                entity.HasKey(e => e.LoginId);

                entity.ToTable("ForgotPassword", "dbo");

                entity.Property(e => e.LoginId).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Login)
                    .WithOne(p => p.ForgotPassword)
                    .HasForeignKey<ForgotPassword>(d => d.LoginId)
                    .HasConstraintName("FK_ForgotPassword_Logins");
            });

            modelBuilder.Entity<Logins>(entity =>
            {
                entity.HasKey(e => e.LoginId);

                entity.ToTable("Logins", "dbo");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.RollNo).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(200);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Logins_UserRoles");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Logins_UserProfile");
            });

            modelBuilder.Entity<Lookup>(entity =>
            {
                entity.ToTable("Lookup", "dbo");

                entity.Property(e => e.LookupType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LookupValue)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Notifications>(entity =>
            {
                entity.HasKey(e => e.NotificationId);

                entity.ToTable("Notifications", "dbo");

                entity.Property(e => e.TargetScreen).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Notifications_UserProfile");
            });

            modelBuilder.Entity<TimeTable>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FileUrl).IsRequired();

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Notification)
                    .WithMany(p => p.TimeTable)
                    .HasForeignKey(d => d.NotificationId)
                    .HasConstraintName("FK_TimeTable_Notifications");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("UserProfile", "dbo");

                entity.Property(e => e.Address).HasMaxLength(150);

                entity.Property(e => e.BatchEnd).HasMaxLength(10);

                entity.Property(e => e.BatchStart).HasMaxLength(10);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Phone).HasMaxLength(25);

                entity.Property(e => e.Program).HasMaxLength(10);

                entity.Property(e => e.Section).HasMaxLength(5);

                entity.Property(e => e.Semester).HasMaxLength(10);
            });

            modelBuilder.Entity<UserRoles>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.ToTable("UserRoles", "dbo");

                entity.Property(e => e.RoleId).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
