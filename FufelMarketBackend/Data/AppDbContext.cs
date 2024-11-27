using System;
using System.Collections.Generic;
using FufelMarketBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace FufelMarketBackend.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ad> Ads { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum("Role", new[] { "USER", "ADMIN" });

        modelBuilder.Entity<Ad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Ad_pkey");

            entity.ToTable("Ad");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.City).HasColumnName("city");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Published)
                .HasDefaultValue(false)
                .HasColumnName("published");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Views)
                .HasDefaultValue(0)
                .HasColumnName("views");

            entity.HasOne(d => d.User).WithMany(p => p.Ads)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Ad_userId_fkey");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Feedback_pkey");

            entity.ToTable("Feedback");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdId).HasColumnName("adId");
            entity.Property(e => e.AdOwnerId).HasColumnName("adOwnerId");
            entity.Property(e => e.AuthorId).HasColumnName("authorId");
            entity.Property(e => e.Score).HasColumnName("score");
            entity.Property(e => e.Text).HasColumnName("text");

            entity.HasOne(d => d.Ad).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.AdId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Feedback_adId_fkey");

            entity.HasOne(d => d.AdOwner).WithMany(p => p.FeedbackAdOwners)
                .HasForeignKey(d => d.AdOwnerId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Feedback_adOwnerId_fkey");

            entity.HasOne(d => d.Author).WithMany(p => p.FeedbackAuthors)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Feedback_authorId_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "User_email_key").IsUnique();

            entity.HasIndex(e => e.Phone, "User_phone_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.Role)
                .HasDefaultValueSql("'USER'::\"Role\"")
                .HasColumnName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
