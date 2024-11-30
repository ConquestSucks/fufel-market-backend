﻿using FufelMarketBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace FufelMarketBackend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Advertisement> Ads { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<SubCategory> SubCategories { get; set; }

    public virtual DbSet<City> Citys { get; set; }
}
