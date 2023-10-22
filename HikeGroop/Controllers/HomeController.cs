﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HikeGroop.Models;
using HikeGroop.Interfaces;
using HikeGroop.Helpers;
using HikeGroop.ViewModels;
using Microsoft.Extensions.Options;
using System.Net;
using System.Globalization;
using Newtonsoft.Json;

namespace HikeGroop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IGroupRepository _groupRepository;
    private readonly IDestinationRepository _destinationRepository;
    private readonly IPInfoSettings _ipInfoOptions;

    public HomeController(
    ILogger<HomeController> logger,
    IGroupRepository groupRepository,
    IDestinationRepository destinationRepository,
    IOptions<IPInfoSettings> ipInfoOptions
    )
    {
        _logger = logger;
        _groupRepository = groupRepository;
        _destinationRepository = destinationRepository;
        _ipInfoOptions = ipInfoOptions.Value;
    }

    public async Task<IActionResult> Index()
    {
        _ = new IPInfo();
        var homeViewModel = new HomeViewModel();

        try
        {
            var url = $"https://ipinfo.io?token={_ipInfoOptions.Token}";
            var info = new WebClient().DownloadString(url);
            IPInfo? ipInfo = JsonConvert.DeserializeObject<IPInfo>(info);

            RegionInfo regionInfo = new RegionInfo(ipInfo.Country);

            ipInfo.Country = regionInfo.EnglishName;

            homeViewModel.City = ipInfo.City;
            homeViewModel.Region = ipInfo.Region;


            if (homeViewModel.City != null)
            {
                homeViewModel.Groups = await _groupRepository
                .GetGroupsByCity(homeViewModel.City);
            }
            else
            {
                homeViewModel.Groups = null;
            }
            return View(homeViewModel);
        }
        catch (System.Exception)
        {
            homeViewModel.Groups = null;
        }
        return View(homeViewModel);
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