﻿// <auto-generated />
using System;
using FufelMarketBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FufelMarketBackend.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FufelMarketBackend.Models.Ad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("category");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("city");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp(3) without time zone")
                        .HasColumnName("createdAt")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Image")
                        .HasColumnType("text")
                        .HasColumnName("image");

                    b.Property<int?>("Price")
                        .HasColumnType("integer")
                        .HasColumnName("price");

                    b.Property<bool>("Published")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("published");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp(3) without time zone")
                        .HasColumnName("updatedAt");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("userId");

                    b.Property<int>("Views")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0)
                        .HasColumnName("views");

                    b.HasKey("Id")
                        .HasName("Ad_pkey");

                    b.HasIndex("UserId");

                    b.ToTable("Ad", (string)null);
                });

            modelBuilder.Entity("FufelMarketBackend.Models.Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AdId")
                        .HasColumnType("integer")
                        .HasColumnName("adId");

                    b.Property<int>("AdOwnerId")
                        .HasColumnType("integer")
                        .HasColumnName("adOwnerId");

                    b.Property<int>("AuthorId")
                        .HasColumnType("integer")
                        .HasColumnName("authorId");

                    b.Property<double>("Score")
                        .HasColumnType("double precision")
                        .HasColumnName("score");

                    b.Property<string>("Text")
                        .HasColumnType("text")
                        .HasColumnName("text");

                    b.HasKey("Id")
                        .HasName("Feedback_pkey");

                    b.HasIndex("AdId");

                    b.HasIndex("AdOwnerId");

                    b.HasIndex("AuthorId");

                    b.ToTable("Feedback", (string)null);
                });

            modelBuilder.Entity("FufelMarketBackend.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Phone")
                        .HasColumnType("text")
                        .HasColumnName("phone");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("User_pkey");

                    b.HasIndex(new[] { "Email" }, "User_email_key")
                        .IsUnique();

                    b.HasIndex(new[] { "Phone" }, "User_phone_key")
                        .IsUnique();

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("FufelMarketBackend.Models.Ad", b =>
                {
                    b.HasOne("FufelMarketBackend.Models.User", "User")
                        .WithMany("Ads")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("Ad_userId_fkey");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FufelMarketBackend.Models.Feedback", b =>
                {
                    b.HasOne("FufelMarketBackend.Models.Ad", "Ad")
                        .WithMany("Feedbacks")
                        .HasForeignKey("AdId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("Feedback_adId_fkey");

                    b.HasOne("FufelMarketBackend.Models.User", "AdOwner")
                        .WithMany("FeedbackAdOwners")
                        .HasForeignKey("AdOwnerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("Feedback_adOwnerId_fkey");

                    b.HasOne("FufelMarketBackend.Models.User", "Author")
                        .WithMany("FeedbackAuthors")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("Feedback_authorId_fkey");

                    b.Navigation("Ad");

                    b.Navigation("AdOwner");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("FufelMarketBackend.Models.Ad", b =>
                {
                    b.Navigation("Feedbacks");
                });

            modelBuilder.Entity("FufelMarketBackend.Models.User", b =>
                {
                    b.Navigation("Ads");

                    b.Navigation("FeedbackAdOwners");

                    b.Navigation("FeedbackAuthors");
                });
#pragma warning restore 612, 618
        }
    }
}
