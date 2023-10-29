using cloudscribe.Pagination.Models;
using HikeGroop.Helpers;
using HikeGroop.Models;

namespace HikeGroop.Interfaces
{
    public interface IGroupRepository
    {
        Task<IEnumerable<Group>> GetGroups();
        Task<PagedResult<Group>> GetGroupsPerPage(PaginationParams paginationParams, string city);
        Task<Group> GetGroupByIdAsync(int id);
        Task<Group> GetGroupByIdAsyncNoTracking(int id);
        Task<IEnumerable<Group>> GetGroupsByCity(string city);
        Task<bool> Add(Group group);
        Task<bool> Update(Group group);
        Task<bool> Delete(Group group);
        Task<bool> Save();
    }
}
