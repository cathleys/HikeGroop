

using HikeGroop.Data;
using HikeGroop.Extensions;
using HikeGroop.Interfaces;
using HikeGroop.Models;
using Microsoft.EntityFrameworkCore;

namespace HikeGroop.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AppUser> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByIdNoTracking(string id)
        {
            return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<Destination>> GetUserDestinations()
        {// i need to access the user info from web page
            var currentUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var getUserDestinations = _context.Destinations
            .Where(d => d.AppUser.Id == currentUser);

            return await getUserDestinations.ToListAsync();
        }

        public async Task<List<Group>> GetUserGroups()
        {
            var currentUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var getUserGroups = _context.Groups
            .Where(g => g.AppUser.Id == currentUser);

            return await getUserGroups.ToListAsync();
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
}