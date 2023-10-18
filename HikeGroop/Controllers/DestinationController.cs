using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HikeGroop.Controllers
{
    public class DestinationController : Controller
    {
        private readonly DataContext _context;

        public DestinationController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var dest = _context.Destinations.ToList();
            return View(dest);
        }
    }
}
