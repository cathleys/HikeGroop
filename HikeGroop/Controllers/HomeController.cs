using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HikeGroop.Models;
using HikeGroop.Interfaces;

namespace HikeGroop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IGroupRepository _groupRepository;

    public HomeController(ILogger<HomeController> logger, IGroupRepository groupRepository)
    {
        _logger = logger;
       _groupRepository = groupRepository;
    }

    public async Task<IActionResult> Index()
    {
        var groups = await _groupRepository.GetGroups();
        return View(groups);
    }  
    

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
