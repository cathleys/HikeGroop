using HikeGroop.Models;

namespace HikeGroop.ViewModels
{
    public class EditGroupViewModel
    {
        public int Id { get; set; }       
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Url { get; set; }
        public IFormFile Image { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }

    }
}
