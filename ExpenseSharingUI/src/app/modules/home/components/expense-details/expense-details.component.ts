import { Component, OnInit } from '@angular/core';
import { ExpenseService } from '../../services/expense.service';
import { ActivatedRoute } from '@angular/router';
import { Expense } from '../../../../core/models/expense.model';
import { ExpenseSplit } from '../../../../core/models/expense-split.model';
import { User } from '../../../../core/models/user.model';
import { AuthService } from '../../../auth/services/auth.service';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-expense-details',
  templateUrl: './expense-details.component.html',
  styleUrls: ['./expense-details.component.css']
})
export class ExpenseDetailsComponent implements OnInit {

  expenseDetails !: ExpenseSplit;
  expenseSplits: Expense[] = [];
  paidByUser!: User;

  loggedInUserId : string = '';


  constructor(private expenseService:ExpenseService, private activatedRoute:ActivatedRoute, private authService : AuthService, private toast : NgToastService) { }

  ngOnInit() {
    this.loggedInUserId = this.authService.getUserIdFromToken();
    let expenseId = this.activatedRoute.snapshot.paramMap.get('id');
    expenseId && this.expenseService.getExpenseById(expenseId).subscribe(
      (res : ExpenseSplit) => {
        this.expenseDetails = res;
        this.expenseSplits = res.expenseSplits;
        this.paidByUser = res.paidByUser
        console.log("expenseDetails:",this.expenseDetails);
        console.log("expenseSplits:", this.expenseSplits);
        console.log("paidByUser:", this.paidByUser);
      }
    )
  }

  settleExpense(splittedUserId:string){
    if(splittedUserId != this.loggedInUserId){
      //console.log("login first");
      this.toast.danger("User must be loggedIn to settle the expense!", "ERROR", 5000)
    }
    else{
      this.expenseService.settleExpense(this.expenseDetails.id, splittedUserId).subscribe(
        response => {
          this.toast.success("Expense settled successfully!", "SUCCESS", 5000)
        },
        error => {
          console.error("Error settling expense:", error);
          alert("Failed to settle the expense.");
        }
       )
    }
  }
}

