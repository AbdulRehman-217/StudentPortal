using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StudentPortal.Models.Models
{
    public partial class StudentPortalContext : DbContext
    {
        public StudentPortalContext()
        {
        }

        public StudentPortalContext(DbContextOptions<StudentPortalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ForgotPassword> ForgotPassword { get; set; }
        public virtual DbSet<Logins> Logins { get; set; }
        public virtual DbSet<Lookup> Lookup { get; set; }
        public virtual DbSet<UserProfile> UserProfile { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=StudentPortal.mssql.somee.com; Database=StudentPortal; User Id=ShanAhmad_SQLLogin_1; password=izinupop7p;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ForgotPassword>(entity =>
            {
                entity.HasKey(e => e.LoginId);

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

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

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
                entity.Property(e => e.LookupType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LookupValue)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.Address1).HasMaxLength(150);

                entity.Property(e => e.Address2).HasMaxLength(150);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Phone).HasMaxLength(25);

                entity.Property(e => e.ZipCode).HasMaxLength(10);
            });

            modelBuilder.Entity<UserRoles>(entity =>
            {
                entity.HasKey(e => e.RoleId);

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
