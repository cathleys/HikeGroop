﻿using HikeGroop.Data.Enums;
using HikeGroop.Models;

namespace HikeGroop.ViewModels;

public class UserViewModel
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string? ProfileImageUrl { get; set; }
    public HikerType? HikerType { get; set; }

}
