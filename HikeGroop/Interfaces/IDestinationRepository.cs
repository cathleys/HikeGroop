using HikeGroop.Models;

namespace HikeGroop.Interfaces
{
    public interface IDestinationRepository
    {
        Task<IEnumerable<Destination>> GetDestinations();
        Task<Destination> GetDestinationByIdAsync(int id);
        Task<bool> Add(Destination destination);
        Task<bool> Update(Destination destination);
        Task<bool> Delete(Destination destination);
        Task<bool> Save();
    }
}
