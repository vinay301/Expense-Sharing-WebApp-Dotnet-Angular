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

        public ExpenseController(IExpenseService expenseService)
        {
            this._expenseService = expenseService;
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

        [HttpPost("SettleExpense")]
        public async Task<IActionResult> SettleExpense([FromBody] SettleExpenseRequestDto settleExpenseRequestDto)
        {
            await _expenseService.SettleExpenseAsync(settleExpenseRequestDto);
            return Ok();
        }

        [HttpGet("group/{groupId}/balances")]
        public async Task<IActionResult> GetGroupBalances(string groupId)
        {
            var result = await _expenseService.GetGroupBalancesAsync(groupId);
            return Ok(result);
        }

    }
}
