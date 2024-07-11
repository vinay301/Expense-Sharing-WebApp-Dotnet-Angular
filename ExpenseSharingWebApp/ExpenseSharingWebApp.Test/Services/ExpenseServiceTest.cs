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

namespace ExpenseSharingWebApp.Test.Service
{
    public class ExpenseServiceTest
    {
        private readonly Mock<IExpenseRepository> _mockExpenseRepository;
        private readonly Mock<IGroupRepository> _mockGroupRepository;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ExpenseService _expenseService;

        public ExpenseServiceTest()
        {
            _mockExpenseRepository = new Mock<IExpenseRepository>();
            _mockGroupRepository = new Mock<IGroupRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockMapper = new Mock<IMapper>();
            _expenseService = new ExpenseService(_mockExpenseRepository.Object, _mockGroupRepository.Object, _mockMapper.Object, _mockUserRepository.Object);
        }

        [Fact]
        public async Task CreateExpenseAsync_ValidRequest_ReturnsExpenseResponseDto()
        {
            // Arrange
            var createExpenseRequestDto = new CreateExpenseRequestDto
            {
                Description = "Test Expense",
                Amount = 100,
                GroupId = "group1",
                PaidByUserId = "user1",
                SplitWithUserIds = new List<string> { "user1", "user2" }
            };

            var expense = new Expense
            {
                Id = "expense1",
                Description = "Test Expense",
                Amount = 100,
                Date = DateTime.UtcNow,
                GroupId = "group1",
                PaidByUserId = "user1",
                ExpenseSplits = new List<ExpenseSplit>()
            };

            var expenseResponseDto = new ExpenseResponseDto
            {
                Id = "expense1",
                Description = "Test Expense",
                Amount = 100
            };

            _mockMapper.Setup(m => m.Map<Expense>(It.IsAny<CreateExpenseRequestDto>())).Returns(expense);
            _mockMapper.Setup(m => m.Map<ExpenseResponseDto>(It.IsAny<Expense>())).Returns(expenseResponseDto);
            _mockGroupRepository.Setup(r => r.GetGroupByIdAsync(It.IsAny<string>())).ReturnsAsync(new Group());
            _mockGroupRepository.Setup(r => r.GetUserByIdAsync(It.IsAny<string>())).ReturnsAsync(new User());
            _mockExpenseRepository.Setup(r => r.CreateExpenseAsync(It.IsAny<Expense>())).ReturnsAsync(expense);

            // Act
            var result = await _expenseService.CreateExpenseAsync(createExpenseRequestDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expenseResponseDto.Id, result.Id);
            Assert.Equal(expenseResponseDto.Description, result.Description);
            Assert.Equal(expenseResponseDto.Amount, result.Amount);
        }

        [Fact]
        public async Task GetExpenseByIdAsync_ExistingExpense_ReturnsExpenseResponseDto()
        {
            // Arrange
            var expenseId = "expense1";
            var expense = new Expense
            {
                Id = expenseId,
                Description = "Test Expense",
                Amount = 100,
                PaidByUserId = "user1",
                ExpenseSplits = new List<ExpenseSplit>()
            };

            var user = new User { Id = "user1", Name = "Test User" };

            _mockExpenseRepository.Setup(r => r.GetExpenseByIdAsync(expenseId)).ReturnsAsync(expense);
            _mockUserRepository.Setup(r => r.GetUserByIdAsync("user1")).ReturnsAsync(user);

            // Act
            var result = await _expenseService.GetExpenseByIdAsync(expenseId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expenseId, result.Id);
            Assert.Equal(expense.Description, result.Description);
            Assert.Equal(expense.Amount, result.Amount);
            Assert.Equal(user.Id, result.PaidByUser.Id);
        }

        
        [Fact]
        public async Task GetAllExpensesByGroupIdAsync_ExistingGroup_ReturnsListOfExpenseResponseDto()
        {
            // Arrange
            var groupId = "group1";
            var expenses = new List<Expense>
            {
                new Expense
                {
                    Id = "expense1",
                    Description = "Expense 1",
                    PaidByUserId = "user1",
                    ExpenseSplits = new List<ExpenseSplit>()
                },
                new Expense
                {
                    Id = "expense2",
                    Description = "Expense 2",
                    PaidByUserId = "user2",
                    ExpenseSplits = new List<ExpenseSplit>()
                }
            };

            _mockExpenseRepository.Setup(r => r.GetAllExpensesByGroupIdAsync(groupId)).ReturnsAsync(expenses);
            _mockUserRepository.Setup(r => r.GetUserByIdAsync(It.IsAny<string>())).ReturnsAsync(new User());

            // Act
            var result = await _expenseService.GetAllExpensesByGroupIdAsync(groupId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("expense1", result[0].Id);
            Assert.Equal("expense2", result[1].Id);
        }

        [Fact]
        public async Task DeleteExpenseAsync_ExistingExpense_CallsRepositoryMethod()
        {
            // Arrange
            var expenseId = "expense1";

            // Act
            await _expenseService.DeleteExpenseAsync(expenseId);

            // Assert
            _mockExpenseRepository.Verify(r => r.DeleteExpenseAsync(expenseId), Times.Once);
        }

        [Fact]
        public async Task UpdateExpenseAsync_ExistingExpense_UpdatesExpense()
        {
            // Arrange
            var expenseId = "expense1";
            var updateExpenseRequestDto = new UpdateExpenseRequestDto
            {
                Description = "Updated Expense",
                Amount = 150,
                PaidByUserId = "user1",
                SplitWithUserIds = new List<string> { "user2", "user3" }
            };
            var existingExpense = new Expense
            {
                Id = expenseId,
                Description = "Original Expense",
                Amount = 100,
                PaidByUserId = "user1",
                ExpenseSplits = new List<ExpenseSplit>()
            };

            _mockExpenseRepository.Setup(r => r.GetExpenseByIdAsync(expenseId)).ReturnsAsync(existingExpense);

            _mockMapper.Setup(m => m.Map(updateExpenseRequestDto, existingExpense))
            .Callback<UpdateExpenseRequestDto, Expense>((dto, expense) => {
                expense.Description = dto.Description;
                expense.Amount = dto.Amount;
                expense.PaidByUserId = dto.PaidByUserId;
            });

            Expense updatedExpense = null;
            _mockExpenseRepository.Setup(r => r.UpdateExpenseAsync(It.IsAny<Expense>()))
                .Callback<Expense>(e => updatedExpense = e);
            // Act
            await _expenseService.UpdateExpenseAsync(expenseId, updateExpenseRequestDto);

            // Assert
            _mockExpenseRepository.Verify(r => r.UpdateExpenseAsync(It.IsAny<Expense>()), Times.Once);

            Assert.NotNull(updatedExpense);
            Assert.Equal(expenseId, updatedExpense.Id);
            Assert.Equal(updateExpenseRequestDto.Description, updatedExpense.Description);
            Assert.Equal(updateExpenseRequestDto.Amount, updatedExpense.Amount);
            Assert.Equal(updateExpenseRequestDto.PaidByUserId, updatedExpense.PaidByUserId);
            Assert.Equal(3, updatedExpense.ExpenseSplits.Count);
        }

        [Fact]
        public async Task SettleExpenseAsync_ValidSettlement_SettlesExpense()
        {
            // Arrange
            var settleExpenseDto = new SettleExpenseRequestDto
            {
                ExpenseId = "expense1",
                SettledByUserId = "user2"
            };
            var expense = new Expense
            {
                Id = "expense1",
                GroupId = "group1",
                ExpenseSplits = new List<ExpenseSplit>
                {
                    new ExpenseSplit { UserId = "user2", PaidToUserId = "user1", AmountOwed = 50, IsSettled = false }
                }
            };
            _mockExpenseRepository.Setup(r => r.GetExpenseByIdAsync("expense1")).ReturnsAsync(expense);
            _mockExpenseRepository.Setup(r => r.GetUserBalanceAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new UserBalance());

            // Act
            await _expenseService.SettleExpenseAsync(settleExpenseDto);

            // Assert
            _mockExpenseRepository.Verify(r => r.UpdateUserBalanceAsync(It.IsAny<UserBalance>()), Times.Exactly(2));
            Assert.True(expense.ExpenseSplits.First().IsSettled);
        }

        [Fact]
        public async Task GetGroupBalancesAsync_ValidGroup_ReturnsBalances()
        {
            // Arrange
            var groupId = "group1";
            var expenses = new List<Expense>
            {
                new Expense
                {
                    Id = "expense1",
                    PaidByUserId = "user1",
                    Amount = 100,
                    ExpenseSplits = new List<ExpenseSplit>
                    {
                        new ExpenseSplit { UserId = "user1", PaidToUserId = "user1", AmountOwed = 50, AmountPaid = 100 },
                        new ExpenseSplit { UserId = "user2", PaidToUserId = "user1", AmountOwed = 50, AmountPaid = 0 }
                    }
                }
            };
            _mockExpenseRepository.Setup(r => r.GetAllExpensesByGroupIdAsync(groupId)).ReturnsAsync(expenses);

            // Act
            var result = await _expenseService.GetGroupBalancesAsync(groupId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(50, result["user1"]);
            Assert.Equal(-50, result["user2"]);
        }
       
        [Fact]
        public async Task GetUserExpensesAsync_ValidUserAndGroup_ReturnsExpenses()
        {
            // Arrange
            var groupId = "group1";
            var userId = "user1";
            var expenses = new List<Expense>
            {
                new Expense { Id = "expense1", Description = "Expense 1" },
                new Expense { Id = "expense2", Description = "Expense 2" }
            };
            var expenseResponseDtos = new List<ExpenseResponseDto>
            {
                new ExpenseResponseDto { Id = "expense1", Description = "Expense 1" },
                new ExpenseResponseDto { Id = "expense2", Description = "Expense 2" }
            };

            _mockExpenseRepository.Setup(r => r.GetUserExpensesAsync(groupId, userId)).ReturnsAsync(expenses);
            _mockMapper.Setup(m => m.Map<List<ExpenseResponseDto>>(expenses)).Returns(expenseResponseDtos);

            // Act
            var result = await _expenseService.GetUserExpensesAsync(groupId, userId);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(expenseResponseDtos[0].Id, result[0].Id);
            Assert.Equal(expenseResponseDtos[1].Id, result[1].Id);
        }

        [Fact]
        public async Task GetExpenseSplitsAsync_ValidUserAndGroup_ReturnsExpenseSplits()
        {
            // Arrange
            var groupId = "group1";
            var userId = "user1";
            var expenseSplits = new List<ExpenseSplit>
            {
                new ExpenseSplit { ExpenseId = "expense1", UserId = userId, AmountOwed = 50 },
                new ExpenseSplit { ExpenseId = "expense2", UserId = userId, AmountOwed = 30 }
            };
            var expenseSplitResponseDtos = new List<ExpenseSplitResponeDto>
            {
                new ExpenseSplitResponeDto { ExpenseId = "expense1", UserShare = 50 },
                new ExpenseSplitResponeDto { ExpenseId = "expense2", UserShare = 30 }
            };

            _mockExpenseRepository.Setup(r => r.GetExpenseSplitsAsync(userId, groupId)).ReturnsAsync(expenseSplits);
            _mockMapper.Setup(m => m.Map<IEnumerable<ExpenseSplitResponeDto>>(expenseSplits)).Returns(expenseSplitResponseDtos);

            // Act
            var result = await _expenseService.GetExpenseSplitsAsync(userId, groupId);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal(expenseSplitResponseDtos.First().ExpenseId, result.First().ExpenseId);
            Assert.Equal(expenseSplitResponseDtos.First().UserShare, result.First().UserShare);
        }
    }
}
