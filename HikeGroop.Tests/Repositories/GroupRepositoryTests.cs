using cloudscribe.Pagination.Models;
using FluentAssertions;
using HikeGroop.Data;
using HikeGroop.Helpers;
using HikeGroop.Models;
using HikeGroop.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HikeGroop.Tests.Repositories;
public class GroupRepositoryTests
{
    private async Task<DataContext> GetDbContext()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;

        var dataContext = new DataContext(options);
        dataContext.Database.EnsureCreated();

        if (await dataContext.Groups.CountAsync() < 0)
        {
            for (int i = 0; i < 10; i++)
            {
                dataContext.Add(

                   new Models.Group
                   {
                       Name = "Mountain Rangers",
                       Description = "No Mountain we cannot move",
                       Image = "https://gttp.imgix.net/368267/x/0/take-photos-at-t%E2%80%A6in-h=210&ixlib=react-9.4.0&q=35&w=920&h=359&dpr=1",
                       Address = new Address
                       {
                           City = "Pasig City"
                       },
                   }
                );

                await dataContext.SaveChangesAsync();
            }
        }
        return dataContext;
    }

    [Fact]
    public async void GroupRepository_Add_ReturnsBool()
    {
        // Arrange
        var group = new Group
        {
            Name = "Mountain Rangers",
            Description = "No Mountain we cannot move",
            Image = "https://gttp.imgix.net/368267/x/0/take-photos-at-t%E2%80%A6in-h=210&ixlib=react-9.4.0&q=35&w=920&h=359&dpr=1",
            Address = new Address
            {
                City = "Pasig City"
            },
        };

        var dataContext = await GetDbContext();
        var groupRepository = new GroupRepository(dataContext);

        // Act
        var result = groupRepository.Add(group);

        // Assert
        result.Should().NotBe(false);

    }

    [Fact]
    public async void GroupRepository_GetGroupByIdAsync_ReturnsGroup()
    {
        // Arrange
        var id = 1;
        var dataContext = await GetDbContext();
        var groupRepository = new GroupRepository(dataContext);

        // Act
        var result = groupRepository.GetGroupByIdAsync(id);
        // Arrange
        result.Should().BeOfType<Task<Group>>();
        result.Should().NotBeNull();
    }


    [Fact]
    public async void GroupRepository_GetGroupPerPage_ReturnsGroups()
    {
        // Arrange
        var paginationParams = new PaginationParams
        {
            PageNumber = 1,
            PageSize = 2,
        };

        var city = "Quezon City";

        var dataContext = await GetDbContext();
        var groupRepository = new GroupRepository(dataContext);

        // Act
        var result = groupRepository.GetGroupsPerPage(paginationParams, city);

        // Assert
        result.Should().BeOfType<Task<PagedResult<Group>>>();
        result.Should().NotBeNull();
    }


    [Fact]
    public async void GroupRepository_Delete_ReturnsBool()
    {
        // Arrange
        var group = new Group
        {
            Name = "Mountain Rangers",
            Description = "No Mountain we cannot move",
            Image = "https://gttp.imgix.net/368267/x/0/take-photos-at-t%E2%80%A6in-h=210&ixlib=react-9.4.0&q=35&w=920&h=359&dpr=1",
            Address = new Address
            {
                City = "Pasig City"
            },
        };
        var dataContext = await GetDbContext();
        var groupRepository = new GroupRepository(dataContext);

        // Act
        var result = groupRepository.Delete(group);

        // Assert
        result.Should().BeOfType<Task<bool>>();
        result.Should().NotBe(false);

    }
}
