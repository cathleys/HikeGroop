using HikeGroop.Models;

namespace HikeGroop.Interfaces;

public interface IDashboardRepository
{
    Task<List<Group>> GetUserGroups();
    Task<List<Destination>> GetUserDestinations();
    Task<AppUser> GetUserById(string id);
    Task<AppUser> GetUserByIdNoTracking(string id);

    Task<bool> Update(AppUser user);
    Task<bool> Save();

}
