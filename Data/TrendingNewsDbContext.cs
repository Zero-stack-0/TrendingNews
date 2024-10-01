using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class TrendingNewsDbContext : DbContext
{
    public TrendingNewsDbContext()
    { }

    public TrendingNewsDbContext(DbContextOptions<TrendingNewsDbContext> options) : base(options)
    { }

    public DbSet<Users> Users { get; set; }
}

