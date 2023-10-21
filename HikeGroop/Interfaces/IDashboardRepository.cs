using HikeGroop.Models;

namespace HikeGroop.Interfaces;

public interface IDashboardRepository
{
    Task<List<Group>> GetUserGroups();
    Task<List<Destination>> GetUserDestinations();

}
