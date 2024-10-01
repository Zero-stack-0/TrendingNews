using System.Runtime.InteropServices.Marshalling;
using Data;
using Data.Repository;
using Entities;
using Microsoft.EntityFrameworkCore;

public class UserRepository : BaseRepository
{
    private readonly TrendingNewsDbContext trendingNewsDbContext;
    public UserRepository(TrendingNewsDbContext trendingNewsDbContext) : base(trendingNewsDbContext)
    {
        this.trendingNewsDbContext = trendingNewsDbContext;
    }

    public async Task<Users?> GetByEmailId(string email)
    {
        return await trendingNewsDbContext.Users.FirstOrDefaultAsync(it => it.EmailId == email);
    }

    public async Task<Users?> GetByUserName(string userName)
    {
        return await trendingNewsDbContext.Users.FirstOrDefaultAsync(it => it.UserName == userName);
    }

    public async Task<Users?> GetByEmailOrUserName(string emailOrUserName)
    {
        return await trendingNewsDbContext.Users.FirstOrDefaultAsync(it => it.EmailId == emailOrUserName || it.UserName == emailOrUserName);
    }
}

