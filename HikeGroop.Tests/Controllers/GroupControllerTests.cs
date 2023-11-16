using System.Net;
using cloudscribe.Pagination.Models;
using FakeItEasy;
using FluentAssertions;
using HikeGroop.Controllers;
using HikeGroop.Helpers;
using HikeGroop.Interfaces;
using HikeGroop.Models;
using HikeGroop.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace HikeGroop.Tests.Controllers;
public class GroupControllerTests
{
    private readonly IUnitOfWork _uow;
    private readonly IPhotoService _photoService;
    private readonly GroupController _groupController;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GroupControllerTests()
    {
        //dependencies
        _uow = A.Fake<IUnitOfWork>();
        _photoService = A.Fake<IPhotoService>();
        _httpContextAccessor = A.Fake<IHttpContextAccessor>();


        //SUT
        _groupController = new GroupController(_uow, _photoService, _httpContextAccessor);
    }

    [Fact]
    public void GroupController_Index_ReturnsSuccess()
    {
        //Arrange - what do I need
        var userParams = new UserParams
        {
            PageNumber = 1,
            PageSize = 2,
            SearchString = "snooze and shoes"
        };


        var groups = A.Fake<PagedResult<Group>>();
        A.CallTo(() => _uow.GroupRepository.GetGroupsPerPage(userParams)).Returns(groups);

        //Act
        var result = _groupController.Index(userParams);

        //Assert - returns what I expect to return
        result.Should().BeOfType<Task<IActionResult>>();
        result.Should().NotBeNull();
    }

    [Fact]
    public void GroupController_Detail_ReturnsSuccess()
    {
        // Arrange
        var id = 1;
        var group = A.Fake<Group>();
        A.CallTo(() => _uow.GroupRepository.GetGroupByIdAsync(id)).Returns(group);

        // Act
        var result = _groupController.Detail(id);


        // Assert
        result.Should().BeOfType<Task<IActionResult>>();
        result.Should().NotBeNull();

    }

    [Fact]
    public void GroupController_Create_ReturnsSuccess()
    {
        // Arrange
        var id = 1;
        var newGroup = new CreateGroupViewModel
        {
            Id = id,
            Name = "Mountain Rangers",
            Description = "We leave no trace but we leave legacy",
            AppUserId = "dgfhghdghghgdhd",
            Address = new Address
            {
                City = "Quezon City"
            }
        };

        // Act
        var result = _groupController.Create(newGroup);

        // Assert
        result.Should().BeOfType<Task<IActionResult>>();
        result.Should().NotBeNull();
    }

    [Fact]
    public void GroupController_Delete_ReturnsSuccess()
    {
        // Arrange
        var id = 1;
        var group = A.Fake<Group>();
        A.CallTo(() => _uow.GroupRepository.GetGroupByIdAsync(id)).Returns(group);


        // Act
        var result = _groupController.Delete(id);

        // Assert
        result.Should().BeOfType<Task<IActionResult>>();

    }

}
