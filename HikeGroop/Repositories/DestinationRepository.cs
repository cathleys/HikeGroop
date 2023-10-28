using cloudscribe.Pagination.Models;
using HikeGroop.Data;
using HikeGroop.Helpers;
using HikeGroop.Interfaces;
using HikeGroop.Models;
using Microsoft.EntityFrameworkCore;

namespace HikeGroop.Repositories
{
    public class DestinationRepository : IDestinationRepository
    {
        private readonly DataContext _context;

        public DestinationRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(Destination destination)
        {
            _context.Add(destination);
            return await Save();
        }

        public Task<bool> Delete(Destination destination)
        {
            _context.Remove(destination);
            return Save();
        }

        public async Task<Destination> GetDestinationByIdAsync(int id)
        {
            return await _context.Destinations
                .Include(g => g.Itinerary)
                .FirstOrDefaultAsync(g => g.Id == id);

        }

        public async Task<Destination> GetDestinationByIdAsyncNoTracking(int id)
        {
            return await _context.Destinations
                .Include(g => g.Itinerary)
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);

        }

        public async Task<IEnumerable<Destination>> GetDestinations()
        {
            return await _context.Destinations.ToListAsync();
        }

        public async Task<PagedResult<Destination>> GetDestinations(PaginationParams paginationParams)
        {
            var query = _context.Destinations.AsQueryable();

            var excludeRecords = (paginationParams.PageSize * paginationParams.PageNumber)
             - paginationParams.PageSize;

            query = query.Skip(excludeRecords).Take(paginationParams.PageSize);


            var result = new PagedResult<Destination>
            {
                Data = await query.AsNoTracking().ToListAsync(),
                TotalItems = await _context.Destinations.CountAsync(),
                PageNumber = paginationParams.PageNumber,
                PageSize = paginationParams.PageSize
            };
            return result;

        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(Destination destination)
        {
            _context.Update(destination);
            return await Save();
            ;
        }
    }
}
