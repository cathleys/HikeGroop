using HikeGroop.Interfaces;
using HikeGroop.Models;
using HikeGroop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HikeGroop.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IPhotoService _photoService;

        public GroupController(IGroupRepository groupRepository,
            IPhotoService photoService)
        {
            _groupRepository = groupRepository;
            _photoService = photoService;
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

       public async Task<IActionResult> Create()
        {
            return View();
;        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGroupViewModel groupVM)
        {
            if (ModelState.IsValid)
            {
                var imageResult = await _photoService.AddPhotoAsync(groupVM.Image);

                var newGroup = new Group
                {
                    Name = groupVM.Name,
                    Description = groupVM.Description,
                    Image = imageResult.SecureUrl.AbsoluteUri,
                    Address = new Address 
                    { 
                        City = groupVM.Address.City
                    }

                };
            await _groupRepository.Add(newGroup);
            return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(groupVM);

;        }
    }
}
