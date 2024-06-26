import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Expense } from '../../../core/models/expense.model';
import { Observable } from 'rxjs';

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

}
