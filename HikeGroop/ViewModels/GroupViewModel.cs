using cloudscribe.Pagination.Models;
using HikeGroop.Models;

namespace HikeGroop.ViewModels;

public class GroupViewModel
{
    public IEnumerable<Group>? Groups { get; set; }
    public string? AppUserName { get; set; }
    public string? AppUserImage { get; set; }


}
