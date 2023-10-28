﻿using cloudscribe.Pagination.Models;
using HikeGroop.Helpers;
using HikeGroop.Models;

namespace HikeGroop.Interfaces
{
    public interface IDestinationRepository
    {
        Task<IEnumerable<Destination>> GetDestinations();
        Task<PagedResult<Destination>> GetDestinations(PaginationParams paginationParams);
        Task<Destination> GetDestinationByIdAsync(int id);
        Task<Destination> GetDestinationByIdAsyncNoTracking(int id);
        Task<bool> Add(Destination destination);
        Task<bool> Update(Destination destination);
        Task<bool> Delete(Destination destination);
        Task<bool> Save();
    }
}
