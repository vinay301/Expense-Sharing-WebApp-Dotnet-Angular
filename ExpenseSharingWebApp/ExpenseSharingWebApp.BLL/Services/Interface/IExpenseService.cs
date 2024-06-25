using ExpenseSharingWebApp.DAL.Models.DTO.Request;
using ExpenseSharingWebApp.DAL.Models.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharingWebApp.BLL.Services.Interface
{
    public interface IExpenseService
    {
        Task<ExpenseResponseDto> CreateExpenseAsync(CreateExpenseRequestDto createExpenseRequestDto);
        Task<ExpenseResponseDto> GetExpenseByIdAsync(string expenseId);
        Task<List<ExpenseResponseDto>> GetAllExpensesByGroupIdAsync(string groupId);
        Task DeleteExpenseAsync(string expenseId);
        Task UpdateExpenseAsync(string expenseId, UpdateExpenseRequestDto updateExpenseRequestDto);
        Task SettleExpenseAsync(SettleExpenseRequestDto settleExpenseRequestDto);
        Task<Dictionary<string, decimal>> GetGroupBalancesAsync(string groupId);
    }
}
