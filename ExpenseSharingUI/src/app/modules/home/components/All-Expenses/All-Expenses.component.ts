import { Component, OnInit, inject } from '@angular/core';
import { GroupService } from '../../services/group.service';
import { ActivatedRoute } from '@angular/router';
import { Group } from '../../../../core/models/group.model';
import { Expense } from '../../../../core/models/expense.model';
import { ExpenseService } from '../../services/expense.service';
import { ExpenseSplit } from '../../../../core/models/expense-split.model';
import { NgToastService } from 'ng-angular-popup';
import { Location } from '@angular/common';

@Component({
  selector: 'app-All-Expenses',
  templateUrl: './All-Expenses.component.html',
  styleUrls: ['./All-Expenses.component.css']
})
export class AllExpensesComponent implements OnInit {

  groupDetails !: Group;
  expenseDetails: { [key: string]: ExpenseSplit } = {};
  expenses: Expense[] = [];
  expId : string ='';
  location = inject(Location)
 
  constructor(private groupService : GroupService, private activeRoute : ActivatedRoute, private expenseService:ExpenseService, private toast : NgToastService) { }

  ngOnInit() {
    let groupId = this.activeRoute.snapshot.paramMap.get('id');
    groupId && this.groupService.getGroupById(groupId).subscribe(
      (res : Group) => {
        this.groupDetails = res;
        this.expenses = res.expenses.map(expense => ({
          ...expense,
          showDetails:false,
          expenseSplits:[]
        }));
        console.log(this.expenses)

        this.expenses.forEach(expense => {
          this.expId = expense.id;
          console.log("expenseId:", this.expId)
        })
      }
    )
  }
  

  getExpenseDetailById(expenseId:string){
    this.expenseService.getExpenseById(expenseId).subscribe(
      (res : ExpenseSplit) => {
        this.expenseDetails[expenseId] = res;
        console.log("expenseDetails:", this.expenseDetails)
      }
    )
  }


  toggleExpenseDetails(expenses: Expense) {
    expenses.showDetails = !expenses.showDetails;
    this.loadExpenseSplits(expenses);
  }

  loadExpenseSplits(expense: Expense) {
   this.expenseService.getExpenseById(expense.id).subscribe((result: ExpenseSplit) => {
          expense.expenseSplits = result.expenseSplits.map((expenseSplits: any) => expenseSplits);
          console.log("expenseSplits:", expense.expenseSplits)
          console.log('ExpenseSplits:', this.expenses);
        });
  }

  deleteExpense(expenseId:string){
    this.expenseService.deleteExpense(expenseId).subscribe(
      () => {
        this.toast.success("SUCCESS", "Expense Deleted Successfully!", 5000)
        setTimeout(()=>{
          window.location.reload();
        },3000)
      }
    
    )
  }

  back(){
    this.location.back();
  }

}
