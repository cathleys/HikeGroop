using HikeGroop.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HikeGroop.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupRepository _groupRepository;

        public GroupController(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<IActionResult> Index()
        {
            var groups = await _groupRepository.GetGroups();
            return  View(groups);
        } 
        
        public async Task<IActionResult> Detail(int id)
        {
            var group = await _groupRepository.GetGroupByIdAsync(id);
                
            return View(group);
        }
    }
}
