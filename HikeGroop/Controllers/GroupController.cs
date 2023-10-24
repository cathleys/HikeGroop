﻿using HikeGroop.Extensions;
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
        private readonly IHttpContextAccessor _httpContextAccessor;


        public GroupController(IGroupRepository groupRepository,
            IPhotoService photoService, IHttpContextAccessor httpContextAccessor
           )
        {
            _groupRepository = groupRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<IActionResult> Index()
        {

            var currentUser = _httpContextAccessor.HttpContext?.User.GetUsername();
            var groups = await _groupRepository.GetGroups();


            var groupViewModel = new GroupViewModel
            {
                Groups = groups,
                AppUserName = currentUser,

            };

            return View(groupViewModel);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var group = await _groupRepository.GetGroupByIdAsync(id);

            return View(group);
        }

        public async Task<IActionResult> Create()
        {
            var currentUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var createGroupViewModel = new CreateGroupViewModel { AppUserId = currentUserId };
            return View(createGroupViewModel);

        }

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
                    AppUserId = groupVM.AppUserId,
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

            ;
        }

        public async Task<IActionResult> Edit(int id)
        {
            var group = await _groupRepository.GetGroupByIdAsync(id);

            if (group == null) return View("Error");

            var groupVM = new EditGroupViewModel
            {
                Name = group.Name,
                Description = group.Description,
                Url = group.Image,
                AddressId = (int)group.AddressId,
                Address = new Address
                {
                    City = group.Address.City
                }

            };

            return View(groupVM);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditGroupViewModel editGroupViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit group");
                return View("Edit", editGroupViewModel);
            }

            var getGroup = await _groupRepository.GetGroupByIdAsyncNoTracking(id);

            if (getGroup != null)
            {
                try
                {
                    var file = new FileInfo(getGroup.Image);
                    var publicId = Path.GetFileNameWithoutExtension(file.Name);

                    await _photoService.DeletePhotoAsync(publicId);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to delete photo");
                    return View(editGroupViewModel);
                }

                var imageResult = await _photoService.AddPhotoAsync(editGroupViewModel.Image);


                var editGroup = new Group
                {
                    Id = id,
                    Name = editGroupViewModel.Name,
                    Description = editGroupViewModel.Description,
                    Image = imageResult.SecureUrl.AbsoluteUri,
                    AddressId = editGroupViewModel.AddressId,
                    Address = editGroupViewModel.Address


                };

                await _groupRepository.Update(editGroup);
                return RedirectToAction("Index");

            }
            else
            {
                return View(editGroupViewModel);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var group = await _groupRepository.GetGroupByIdAsync(id);
            if (group == null) return View("Error");

            return View(group);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var group = await _groupRepository.GetGroupByIdAsync(id);

            if (group == null) return View("Error");

            if (!string.IsNullOrEmpty(group.Image))
            {
                await _photoService.DeletePhotoAsync(group.Image);
            }

            await _groupRepository.Delete(group);
            return RedirectToAction("Index");
        }
    }
}
