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
    }
}
