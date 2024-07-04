import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Expense } from '../../../core/models/expense.model';
import { Observable } from 'rxjs';
import { ExpenseSplit } from '../../../core/models/expense-split.model';

@Injectable({
  providedIn: 'root'
})
export class ExpenseService {

  baseApiUrl : string = environment.baseApiUrl
constructor(private http : HttpClient) { }

addExpense(expenseObj : Expense):Observable<Expense>{
  return this.http.post<Expense>(this.baseApiUrl + '/api/Expenses/AddExpense',expenseObj)
}

getAllExpensesOfGroup(groupId : string):Observable<Expense[]>{
  return this.http.get<Expense[]>(this.baseApiUrl + `/api/Expenses/GetAllExpenseOfGroup/${groupId}`);
}

getExpenseById(expenseId : string) : Observable<ExpenseSplit>{
  return this.http.get<ExpenseSplit>(this.baseApiUrl + `/api/Expenses/GetExpenseById/${expenseId}`);
}

getGroupBalanceByGroupId(groupId:string) : Observable<{ [key: string]: number }>{
  return this.http.get<{ [key: string]: number }>(this.baseApiUrl + `/api/Expenses/group/balances/${groupId}`);
}

settleExpense(expenseId:string, settledByUserId:string) : Observable<ExpenseSplit>{
  return this.http.post<ExpenseSplit>(this.baseApiUrl + `/api/Expenses/SettleExpense/${expenseId}/${settledByUserId}`,{})
}

deleteExpense(expenseId : string) : Observable<Expense>{
  return this.http.delete<Expense>(this.baseApiUrl + `/api/Expenses/DeleteExpense/${expenseId}`);
}

}
