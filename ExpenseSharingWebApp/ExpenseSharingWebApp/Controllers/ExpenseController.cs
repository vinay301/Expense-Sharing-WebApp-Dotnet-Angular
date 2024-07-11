using ExpenseSharingWebApp.BLL.Services.Interface;
using ExpenseSharingWebApp.DAL.Models.DTO.Request;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseSharingWebApp.Controllers
{
    [ApiController]
    [Route("api/Expenses")]
    public class ExpenseController : Controller
    {
        private readonly IExpenseService _expenseService;
        private readonly ILogger<ExpenseController> _logger;
        public ExpenseController(IExpenseService expenseService, ILogger<ExpenseController> logger)
        {
            this._expenseService = expenseService;
            _logger = logger;
        }

        [HttpPost]
        [Route("AddExpense")]
        public async Task<IActionResult> CreateExpense([FromBody] CreateExpenseRequestDto createExpenseRequestDto)
        {
            var expense = await _expenseService.CreateExpenseAsync(createExpenseRequestDto); //exception here
            return Ok(expense);
            
        }

        [HttpGet("GetExpenseById/{expenseId}")]
        public async Task<IActionResult> GetExpenseById(string expenseId)
        {
            var expense = await _expenseService.GetExpenseByIdAsync(expenseId);
            if (expense == null)
            {
                return NotFound();
            }
            return Ok(expense);
        }

        [HttpGet("GetAllExpenseOfGroup/{groupId}")]
        public async Task<IActionResult> GetAllExpensesByGroupId(string groupId)
        {
            var expenses = await _expenseService.GetAllExpensesByGroupIdAsync(groupId);
            return Ok(expenses);
        }

        [HttpDelete("DeleteExpense/{expenseId}")]
        public async Task<IActionResult> DeleteExpense(string expenseId)
        {
            await _expenseService.DeleteExpenseAsync(expenseId);
            return Ok();
        }

        [HttpPut("EditExpense/{expenseId}")]
        public async Task<IActionResult> UpdateExpense(string expenseId, [FromBody] UpdateExpenseRequestDto updateExpenseRequestDto)
        {
            await _expenseService.UpdateExpenseAsync(expenseId, updateExpenseRequestDto);
            return Ok();
        }

        [HttpPost("SettleExpense/{expenseId}/{settledByUserId}")]
        public async Task<IActionResult> SettleExpense(string expenseId, string settledByUserId)
        {
            if (string.IsNullOrEmpty(expenseId) || string.IsNullOrEmpty(settledByUserId))
            {
                return BadRequest("ExpenseId and SettledByUserId cannot be null or empty.");
            }

            try
            {
                var settleExpenseRequestDto = new SettleExpenseRequestDto
                {
                    ExpenseId = expenseId,
                    SettledByUserId = settledByUserId
                };

                await _expenseService.SettleExpenseAsync(settleExpenseRequestDto);

                return Ok(new { message = "Expense settled successfully." });
            }
            catch (Exception ex)
            {
                // Log the exception (add your logging mechanism here)
                _logger.LogError(ex, "Error occurred while settling expense");
                return StatusCode(500, "Internal server error");
            }
           
        }

        [HttpGet("group/balances/{groupId}")]
        public async Task<IActionResult> GetGroupBalances(string groupId)
        {
            var result = await _expenseService.GetGroupBalancesAsync(groupId);
            return Ok(result);
        }

        [HttpGet]
        [Route("UserExpenses/{groupId}/{userId}")]
        public async Task<IActionResult> GetUserExpenses(string groupId, string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(groupId) || string.IsNullOrEmpty(userId))
                {
                    return BadRequest("Group Id and User Id cannot be null or empty.");
                }

                var expenses = await _expenseService.GetUserExpensesAsync(groupId, userId);
                return Ok(expenses);
            }
            catch (Exception ex)
            {
                // Log the exception details here as per your logging strategy
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetExpenseSplits/{userId}/{groupId}")]
        public async Task<IActionResult> GetExpenseSplits(string userId, string groupId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(groupId))
                {
                    return BadRequest("User Id and Group Id cannot be null or empty.");
                }

                var expenseSplits = await _expenseService.GetExpenseSplitsAsync(userId, groupId);
                return Ok(expenseSplits);
            }
            catch (Exception ex)
            {
                // Log the exception details here as per your logging strategy
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
