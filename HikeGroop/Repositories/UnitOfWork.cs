using HikeGroop.Data;
using HikeGroop.Interfaces;

namespace HikeGroop.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UnitOfWork(DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }
    public IDashboardRepository DashboardRepository => new DashboardRepository(_context, _httpContextAccessor);

    public IDestinationRepository DestinationRepository => new DestinationRepository(_context);

    public IGroupRepository GroupRepository => new GroupRepository(_context);

    public IUserRepository UserRepository => new UserRepository(_context);
}
