using AutoMapper;
using ExpenseSharingWebApp.BLL.Services.Implementation;
using ExpenseSharingWebApp.DAL.Models.Domain;
using ExpenseSharingWebApp.DAL.Models.DTO;
using ExpenseSharingWebApp.DAL.Repositories.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ExpenseSharingWebApp.Test.Services
{
    public class UserServiceTest
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UserService _userService;

        public UserServiceTest()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockMapper = new Mock<IMapper>();
            _userService = new UserService(_mockUserRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllUsersAsync_ReturnsListOfUserDTOs()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = "1", Name = "User1" },
                new User { Id = "2", Name = "User2" }
            };
            var userDTOs = new List<UserDto>
            {
                new UserDto { Id = "1", Name = "User1" },
                new UserDto { Id = "2", Name = "User2" }
            };

            _mockUserRepository.Setup(repo => repo.GetAllUsersAsync()).ReturnsAsync(users);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<UserDto>>(users)).Returns(userDTOs);

            // Act
            var result = await _userService.GetAllUsersAsync();

            // Assert
            Assert.Equal(2, result.Count());
            _mockUserRepository.Verify(repo => repo.GetAllUsersAsync(), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<IEnumerable<UserDto>>(users), Times.Once);
        }
    }
}
