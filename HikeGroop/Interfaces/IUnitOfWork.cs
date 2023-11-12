namespace HikeGroop.Interfaces;
public interface IUnitOfWork
{
    IDashboardRepository DashboardRepository { get; }
    IDestinationRepository DestinationRepository { get; }
    IGroupRepository GroupRepository { get; }
    IUserRepository UserRepository { get; }
}
