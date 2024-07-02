using AutoMapper;
using ExpenseSharingWebApp.BLL.Services.Implementation;
using ExpenseSharingWebApp.DAL.Models.Domain;
using ExpenseSharingWebApp.DAL.Models.DTO.Request;
using ExpenseSharingWebApp.DAL.Models.DTO.Response;
using ExpenseSharingWebApp.DAL.Repositories.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ExpenseSharingWebApp.Test.Repository
{
    public class GroupServiceTest
    {
        private readonly Mock<IGroupRepository> _mockGroupRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GroupService _groupService;
        public GroupServiceTest()
        {
            _mockGroupRepository = new Mock<IGroupRepository>();
            _mockMapper = new Mock<IMapper>();
            _groupService = new GroupService(_mockGroupRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task CreateGroupAsync_ValidRequest_ReturnsGroupResponseDto()
        {
            // Arrange
            var createGroupRequestDto = new CreateGroupRequestDto { Name = "Test Group" };
            var group = new Group { Id = Guid.NewGuid().ToString(), Name = "Test Group" };
            var groupResponseDto = new GroupResponseDto { Id = group.Id, Name = "Test Group" };

            _mockMapper.Setup(m => m.Map<Group>(createGroupRequestDto)).Returns(group);
            _mockGroupRepository.Setup(r => r.CreateGroupAsync(It.IsAny<Group>())).ReturnsAsync(group);
            _mockMapper.Setup(m => m.Map<GroupResponseDto>(group)).Returns(groupResponseDto);

            // Act
            var result = await _groupService.CreateGroupAsync(createGroupRequestDto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<GroupResponseDto>(result);
            Assert.Equal(groupResponseDto.Id, result.Id);
            Assert.Equal(groupResponseDto.Name, result.Name);
        }
    }
}
