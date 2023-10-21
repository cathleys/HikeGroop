

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
    }
}