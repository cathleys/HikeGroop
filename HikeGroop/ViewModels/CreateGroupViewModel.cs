using HikeGroop.Models;

namespace HikeGroop.ViewModels
{
    public class CreateGroupViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }

        public string AppUserId { get; set; }
        public Address? Address { get; set; }
    }
}
