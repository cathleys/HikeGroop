using HikeGroop.Models;
using cloudscribe.Pagination.Models;
using HikeGroop.Helpers;


namespace HikeGroop.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<AppUser>> GetAllUsers();
    Task<PagedResult<AppUser>> GetMembers(PaginationParams paginationParams, string city);
    Task<AppUser> GetUserById(string userId);
    Task<bool> Add(AppUser user);
    Task<bool> Update(AppUser user);
    Task<bool> Delete(AppUser user);
    Task<bool> Save();
}
