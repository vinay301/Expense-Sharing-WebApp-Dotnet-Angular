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
            var createGroupDto = new CreateGroupRequestDto
            {
                Name = "Test Group",
                MemberIds = new List<string> { "user1", "user2" }
            };

            var group = new Group
            {
                Id = "groupId",
                Name = "Test Group",
                CreatedDate = DateTime.UtcNow,
                UserGroups = new List<UserGroup>()
            };
            var groupResponseDto = new GroupResponseDto
            {
                Id = "groupId",
                Name = "Test Group"
            };

            _mockMapper.Setup(m => m.Map<Group>(createGroupDto)).Returns(group);
            _mockMapper.Setup(m => m.Map<GroupResponseDto>(group)).Returns(groupResponseDto);
            _mockGroupRepository.Setup(r => r.GroupExistsAsync(It.IsAny<string>())).ReturnsAsync(false);
            _mockGroupRepository.Setup(r => r.GetUserByIdAsync(It.IsAny<string>())).ReturnsAsync(new User());
            _mockGroupRepository.Setup(r => r.CreateGroupAsync(It.IsAny<Group>())).ReturnsAsync(group);

            // Act
            var result = await _groupService.CreateGroupAsync(createGroupDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(groupResponseDto.Id, result.Id);
            Assert.Equal(groupResponseDto.Name, result.Name);
        }

        [Fact]
        public async Task CreateGroupAsync_GroupExists_ThrowsException()
        {
            // Arrange
            var createGroupDto = new CreateGroupRequestDto
            {
                Name = "Test Group",
                MemberIds = new List<string> { "user1", "user2" }
            };
            var group = new Group
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Test Group",
                CreatedDate = DateTime.UtcNow,
                UserGroups = new List<UserGroup>()
            };

            _mockMapper.Setup(m => m.Map<Group>(It.IsAny<CreateGroupRequestDto>())).Returns(group);
            _mockGroupRepository.Setup(r => r.GroupExistsAsync(It.IsAny<string>())).ReturnsAsync(true);


            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _groupService.CreateGroupAsync(createGroupDto));
            Assert.Equal("GroupId must be unique", exception.Message);
        }

        [Fact]
        public async Task CreateGroupAsync_TooManyMembers_ThrowsException()
        {
            // Arrange
            var createGroupDto = new CreateGroupRequestDto
            {
                Name = "Test Group",
                MemberIds = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11" }
            };

            var group = new Group
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Test Group",
                CreatedDate = DateTime.UtcNow,
                UserGroups = new List<UserGroup>()
            };

            _mockMapper.Setup(m => m.Map<Group>(It.IsAny<CreateGroupRequestDto>())).Returns(group);
            _mockGroupRepository.Setup(r => r.GroupExistsAsync(It.IsAny<string>())).ReturnsAsync(false);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _groupService.CreateGroupAsync(createGroupDto));
            Assert.Equal("Group cannot have more than 10 members", exception.Message);
           
        }

        [Fact]
        public async Task GetGroupByIdAsync_ExistingGroup_ReturnsGroupResponseDto()
        {
            // Arrange
            var groupId = "groupId";
            var group = new Group { Id = groupId, Name = "Test Group" };
            var groupResponseDto = new GroupResponseDto { Id = groupId, Name = "Test Group" };

            _mockGroupRepository.Setup(r => r.GetGroupByIdAsync(groupId)).ReturnsAsync(group);
            _mockMapper.Setup(m => m.Map<GroupResponseDto>(group)).Returns(groupResponseDto);

            // Act
            var result = await _groupService.GetGroupByIdAsync(groupId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(groupResponseDto.Id, result.Id);
            Assert.Equal(groupResponseDto.Name, result.Name);
        }

        [Fact]
        public async Task GetAllGroupsAsync_GroupsExist_ReturnsListOfGroupResponseDto()
        {
            // Arrange
            var groups = new List<Group>
            {
                new Group { Id = "group1", Name = "Group 1" },
                new Group { Id = "group2", Name = "Group 2" }
            };

            var groupResponseDtos = new List<GroupResponseDto>
            {
                new GroupResponseDto { Id = "group1", Name = "Group 1" },
                new GroupResponseDto { Id = "group2", Name = "Group 2" }
            };

            _mockGroupRepository.Setup(r => r.GetAllGroupsAsync()).ReturnsAsync(groups);
            _mockMapper.Setup(m => m.Map<List<GroupResponseDto>>(groups)).Returns(groupResponseDtos);

            // Act
            var result = await _groupService.GetAllGroupsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task DeleteGroupAsync_ExistingGroup_CallsRepositoryMethods()
        {
            // Arrange
            var groupId = "groupId";

            // Act
            await _groupService.DeleteGroupAsync(groupId);

            // Assert
            _mockGroupRepository.Verify(r => r.DeleteUserGroupsByGroupIdAsync(groupId), Times.Once);
            _mockGroupRepository.Verify(r => r.DeleteGroupAsync(groupId), Times.Once);
        }

        [Fact]
        public async Task DeleteUserFromGroupAsync_UserExistsInGroup_RemovesUser()
        {
            // Arrange
            var groupId = "groupId";
            var userId = "userId";
            var group = new Group
            {
                Id = groupId,
                UserGroups = new List<UserGroup>
                {
                    new UserGroup { UserId = userId }
                }
            };
            _mockGroupRepository.Setup(r => r.GetGroupByIdAsync(groupId)).ReturnsAsync(group);

            // Act
            await _groupService.DeleteUserFromGroupAsync(groupId, userId);

            // Assert
            Assert.Empty(group.UserGroups);
            _mockGroupRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteUserFromGroupAsync_UserNotInGroup_ThrowsException()
        {
            // Arrange
            var groupId = "groupId";
            var userId = "userId";
            var group = new Group
            {
                Id = groupId,
                UserGroups = new List<UserGroup>()
            };

            _mockGroupRepository.Setup(r => r.GetGroupByIdAsync(groupId)).ReturnsAsync(group);
            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _groupService.DeleteUserFromGroupAsync(groupId, userId));
        }

    }
    }
