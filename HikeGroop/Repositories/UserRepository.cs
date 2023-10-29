using HikeGroop.Data;
using HikeGroop.Interfaces;
using HikeGroop.Models;
using Microsoft.EntityFrameworkCore;
using cloudscribe.Pagination.Models;
using HikeGroop.Helpers;


namespace HikeGroop.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<bool> Add(AppUser user)
    {
        _context.Add(user);
        return await Save();
    }

    public async Task<bool> Delete(AppUser user)
    {
        _context.Remove(user);
        return await Save();
    }

    public async Task<IEnumerable<AppUser>> GetAllUsers()
    {
        return await _context.Users.OrderBy(u => u.Id).ToListAsync();
    }
    public async Task<PagedResult<AppUser>> GetMembers(PaginationParams paginationParams,
    string city)
    {
        var query = _context.Users.AsQueryable();

        int excludeRecords = (paginationParams.PageSize * paginationParams.PageNumber) - paginationParams.PageSize;

        var userCount = await query.CountAsync();

        if (!string.IsNullOrEmpty(city))
        {
            query = query.Where(d => d.City.Contains(city));
            userCount = await query.CountAsync();
        }

        query = query.OrderBy(u => u.Id)
        .Skip(excludeRecords)
        .Take(paginationParams.PageSize);


        var result = new PagedResult<AppUser>
        {
            Data = await query.AsNoTracking().ToListAsync(),
            TotalItems = userCount,
            PageNumber = paginationParams.PageNumber,
            PageSize = paginationParams.PageSize
        };

        return result;
    }

    public async Task<AppUser> GetUserById(string userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task<bool> Save()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Update(AppUser user)
    {
        _context.Update(user);
        return await Save();
    }
}
