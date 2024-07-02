using AutoMapper;
using ExpenseSharingWebApp.BLL.Services.Interface;
using ExpenseSharingWebApp.Controllers;
using ExpenseSharingWebApp.DAL.Models.Domain;
using ExpenseSharingWebApp.DAL.Models.DTO.Request;
using ExpenseSharingWebApp.DAL.Models.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ExpenseSharingWebApp.Test.Controllers
{
    public class GroupControllerTest
    {
        private readonly Mock<IGroupService> _mockGroupService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GroupController _controller;

        public GroupControllerTest()
        {
            _mockGroupService = new Mock<IGroupService>();
            _mockMapper = new Mock<IMapper>();
            _controller = new GroupController(_mockGroupService.Object, _mockMapper.Object);
        }
        [Fact]
        public async Task CreateGroup_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var createGroupRequestDto = new CreateGroupRequestDto
            {
                Name = "Test Group"
            };
            var groupResponseDto = new GroupResponseDto
            {
                Id = Guid.NewGuid().ToString(),
                Name = createGroupRequestDto.Name
            };

            _mockGroupService.Setup(s => s.CreateGroupAsync(createGroupRequestDto))
                .ReturnsAsync(groupResponseDto);


            // Act
            var result = await _controller.CreateGroup(createGroupRequestDto);

            // Assert

            Assert.NotNull(result); // Ensure the result is not null
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value); // Ensure the Value is not null
            var returnedGroup = Assert.IsType<GroupResponseDto>(okResult.Value);
            Assert.Equal(groupResponseDto.Id, returnedGroup.Id);
            Assert.Equal(groupResponseDto.Name, returnedGroup.Name);
        }

        [Fact]
        public async Task CreateGroup_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("Error", "Model is invalid");

            // Act
            var result = await _controller.CreateGroup(new CreateGroupRequestDto());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateGroup_ExceptionThrown_ReturnsBadRequest()
        {
            // Arrange
            var createGroupRequestDto = new CreateGroupRequestDto();
            _mockGroupService.Setup(s => s.CreateGroupAsync(createGroupRequestDto))
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.CreateGroup(createGroupRequestDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Test exception", badRequestResult.Value);
        }

        [Fact]
        public async Task GetAllGroups_ReturnsOkResult()
        {
            // Arrange
            var groups = new List<GroupResponseDto>();
            _mockGroupService.Setup(s => s.GetAllGroupsAsync())
                .ReturnsAsync(groups);

            // Act
            var result = await _controller.GetAllGroups();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(groups, okResult.Value);
        }

        [Fact]
        public async Task GetGroupById_ExistingGroup_ReturnsOkResult()
        {
            //Arrange
            var groupId = "test-group-id";
            var group = new GroupResponseDto()
            {
                Id = groupId,
                Name = "Test-Group"
            };
            _mockGroupService.Setup(s => s.GetGroupByIdAsync(groupId)).ReturnsAsync(group);

            //Act
            var result = await _controller.GetGroupByIdAsync(groupId);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedGroup = Assert.IsType<GroupResponseDto>(okResult.Value);

            Assert.Equal(group.Id, returnedGroup.Id);
            Assert.Equal(group.Name, returnedGroup.Name);
        }

        [Fact]
        public async Task GetGroupById_NonExistingGroup_ReturnsNotFound()
        {
            //Arrange
            var groupId = "non-existing-id";
            _mockGroupService.Setup(s => s.GetGroupByIdAsync(groupId)).ReturnsAsync((GroupResponseDto)null);

            //Act
            var result = await _controller.GetGroupByIdAsync(groupId);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task AddUsersToGroup_ReturnsOkResult()
        {
            // Arrange
            var groupId = "testGroupId";
            var userIds = new List<string> { "user1", "user2" };

            // Act
            var result = await _controller.AddUsersToGroup(groupId, userIds);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockGroupService.Verify(s => s.AddUsersToGroupAsync(groupId, userIds), Times.Once);
        }
        [Fact]
        public async Task DeleteGroup_ReturnsOkResult()
        {
            // Arrange
            var groupId = "testGroupId";

            // Act
            var result = await _controller.DeleteGroup(groupId);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockGroupService.Verify(s => s.DeleteGroupAsync(groupId), Times.Once);
        }

        [Fact]
        public async Task DeleteUserInGroup_ReturnsOkResult()
        {
            // Arrange
            var groupId = "testGroupId";
            var userId = "testUserId";

            // Act
            var result = await _controller.DeleteUserInGroup(groupId, userId);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockGroupService.Verify(s => s.DeleteUserFromGroupAsync(groupId, userId), Times.Once);
        }
    }
}
