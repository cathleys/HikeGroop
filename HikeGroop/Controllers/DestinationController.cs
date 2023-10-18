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

        public IActionResult Detail(int id)
        {
            var dest = _context.Destinations
                .Include(d=> d.Itinerary)
                .FirstOrDefault(d=> d.Id ==id);
            return View(dest);
        }
    }
}
