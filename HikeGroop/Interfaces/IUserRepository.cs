using HikeGroop.Models;

namespace HikeGroop.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<AppUser>> GetAllUsers();
    Task<AppUser> GetUserById(string userId);
    Task<bool> Add(AppUser user);
    Task<bool> Update(AppUser user);
    Task<bool> Delete(AppUser user);
    Task<bool> Save();
}
