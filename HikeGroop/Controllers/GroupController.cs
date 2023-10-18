using Microsoft.AspNetCore.Mvc;

namespace HikeGroop.Controllers
{
    public class GroupController : Controller
    {
        private readonly DataContext _context;

        public GroupController(DataContext context)
        {
          _context = context;
        }

        public IActionResult Index()
        {
            var groups = _context.Groups.ToList();
            return View(groups);
        }
    }
}
