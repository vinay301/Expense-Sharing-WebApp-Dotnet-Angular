using ExpenseSharingWebApp.BLL.Services.Interface;
using ExpenseSharingWebApp.Controllers;
using ExpenseSharingWebApp.DAL.Models.DTO.Request;
using ExpenseSharingWebApp.DAL.Models.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ExpenseSharingWebApp.Test.Controllers
{
    public class ExpenseControllerTest
    {
        private readonly Mock<IExpenseService> _mockExpenseService;
        private readonly Mock<ILogger<ExpenseController>> _loggerMock;
        private readonly ExpenseController _controller;

        public ExpenseControllerTest()
        {
            _mockExpenseService = new Mock<IExpenseService>();
            _loggerMock = new Mock<ILogger<ExpenseController>>();
            _controller = new ExpenseController(_mockExpenseService.Object, _loggerMock.Object);

        }

        [Fact]
        public async Task CreateExpense_ReturnsOkResult()
        {
            // Arrange
            var createExpenseRequestDto = new CreateExpenseRequestDto();
            var expenseResponseDto = new ExpenseResponseDto();
            _mockExpenseService.Setup(s => s.CreateExpenseAsync(createExpenseRequestDto)).ReturnsAsync(expenseResponseDto);

            // Act
            var result = await _controller.CreateExpense(createExpenseRequestDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expenseResponseDto, okResult.Value);
        }

        [Fact]
        public async Task GetExpenseById_ExistingExpense_ReturnsOkResult()
        {
            // Arrange
            var expenseId = "testId";
            var expenseResponseDto = new ExpenseResponseDto();
            _mockExpenseService.Setup(s => s.GetExpenseByIdAsync(expenseId)).ReturnsAsync(expenseResponseDto);

            // Act
            var result = await _controller.GetExpenseById(expenseId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expenseResponseDto, okResult.Value);
        }

        [Fact]
        public async Task GetExpenseById_NonExistingExpense_ReturnsNotFound()
        {
            // Arrange
            var expenseId = "nonExistingId";
            _mockExpenseService.Setup(s => s.GetExpenseByIdAsync(expenseId)).ReturnsAsync((ExpenseResponseDto)null);

            // Act
            var result = await _controller.GetExpenseById(expenseId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAllExpensesByGroupId_ReturnsOkResult()
        {
            // Arrange
            var groupId = "testGroupId";
            var expenses = new List<ExpenseResponseDto>();
            _mockExpenseService.Setup(s => s.GetAllExpensesByGroupIdAsync(groupId)).ReturnsAsync(expenses);

            // Act
            var result = await _controller.GetAllExpensesByGroupId(groupId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expenses, okResult.Value);
        }

        [Fact]
        public async Task DeleteExpense_ReturnsOkResult()
        {
            // Arrange
            var expenseId = "testExpenseId";

            // Act
            var result = await _controller.DeleteExpense(expenseId);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockExpenseService.Verify(s => s.DeleteExpenseAsync(expenseId), Times.Once);
        }

        [Fact]
        public async Task UpdateExpense_ReturnsOkResult()
        {
            // Arrange
            var expenseId = "testExpenseId";
            var updateExpenseRequestDto = new UpdateExpenseRequestDto();

            // Act
            var result = await _controller.UpdateExpense(expenseId, updateExpenseRequestDto);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockExpenseService.Verify(s => s.UpdateExpenseAsync(expenseId, updateExpenseRequestDto), Times.Once);
        }

        [Fact]
        public async Task SettleExpense_ValidInput_ReturnsOkResult()
        {
            // Arrange
            var expenseId = "testExpenseId";
            var settledByUserId = "testUserId";
            var settleExpenseRequestDto = new SettleExpenseRequestDto
            {
                ExpenseId = expenseId,
                SettledByUserId = settledByUserId
            };
            _mockExpenseService.Setup(s => s.SettleExpenseAsync(settleExpenseRequestDto)).Returns(Task.CompletedTask);
            // Act
            var result = await _controller.SettleExpense(expenseId, settledByUserId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var responseValue = okResult.Value;
            var expectedResponse = new { message = "Expense settled successfully." };
            Assert.Equal(expectedResponse.message, responseValue.GetType().GetProperty("message").GetValue(responseValue, null).ToString());
           
        }

        [Fact]
        public async Task SettleExpense_InvalidInput_ReturnsBadRequest()
        {
            // Arrange
            var expenseId = "";
            var settledByUserId = "";

            // Act
            var result = await _controller.SettleExpense(expenseId, settledByUserId);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task SettleExpense_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var expenseId = "testExpenseId";
            var settledByUserId = "testUserId";
            _mockExpenseService.Setup(s => s.SettleExpenseAsync(It.IsAny<SettleExpenseRequestDto>()))
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.SettleExpense(expenseId, settledByUserId);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task GetGroupBalances_ReturnsOkResult()
        {
            // Arrange
            var groupId = "testGroupId";
            var balances = new Dictionary<string, decimal>();
            _mockExpenseService.Setup(s => s.GetGroupBalancesAsync(groupId)).ReturnsAsync(balances);

            // Act
            var result = await _controller.GetGroupBalances(groupId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(balances, okResult.Value);
        }

        [Fact]
        public async Task GetUserExpenses_ValidInput_ReturnsOkResult()
        {
            // Arrange
            var groupId = "testGroupId";
            var userId = "testUserId";
            var expenses = new List<ExpenseResponseDto>();
            _mockExpenseService.Setup(s => s.GetUserExpensesAsync(groupId, userId)).ReturnsAsync(expenses);

            // Act
            var result = await _controller.GetUserExpenses(groupId, userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expenses, okResult.Value);
        }

        [Fact]
        public async Task GetUserExpenses_InvalidInput_ReturnsBadRequest()
        {
            // Arrange
            var groupId = "";
            var userId = "";

            // Act
            var result = await _controller.GetUserExpenses(groupId, userId);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetExpenseSplits_ValidInput_ReturnsOkResult()
        {
            // Arrange
            var userId = "testUserId";
            var groupId = "testGroupId";
            var expenseSplits = new List<ExpenseSplitResponeDto>();
            _mockExpenseService.Setup(s => s.GetExpenseSplitsAsync(userId, groupId)).ReturnsAsync(expenseSplits);

            // Act
            var result = await _controller.GetExpenseSplits(userId, groupId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expenseSplits, okResult.Value);
        }

        [Fact]
        public async Task GetExpenseSplits_InvalidInput_ReturnsBadRequest()
        {
            // Arrange
            var userId = "";
            var groupId = "";

            // Act
            var result = await _controller.GetExpenseSplits(userId, groupId);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
