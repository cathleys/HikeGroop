using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        
        public IActionResult Detail(int id)
        {
            var group = _context.Groups
                .Include(g=>g.Address)
                .FirstOrDefault(g=>g.Id == id);
            return View(group);
        }
    }
}
