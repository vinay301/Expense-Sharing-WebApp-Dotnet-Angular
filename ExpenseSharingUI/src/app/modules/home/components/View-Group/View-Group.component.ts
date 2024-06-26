import { Component, OnInit } from '@angular/core';
import { Group } from '../../../../core/models/group.model';
import { GroupService } from '../../services/group.service';
import { ActivatedRoute } from '@angular/router';
import { User } from '../../../../core/models/user.model';
import { Expense } from '../../../../core/models/expense.model';
import { ExpenseService } from '../../services/expense.service';

@Component({
  selector: 'app-View-Group',
  templateUrl: './View-Group.component.html',
  styleUrls: ['./View-Group.component.css']
})
export class ViewGroupComponent implements OnInit {

  groupDetails !: Group;
  members: User[] = [];
  expenses: Expense[] = [];
  totalExpenses: number = 0;
  constructor(private groupService : GroupService, private activeRoute : ActivatedRoute, private expenseService : ExpenseService) { }

  ngOnInit() {
    let groupId = this.activeRoute.snapshot.paramMap.get('id');
    
    //console.warn(groupId);
    groupId && this.groupService.getGroupById(groupId).subscribe(
      (result : Group) => {
        this.groupDetails = result;
        // console.warn(result)
        // console.log("members:", result.userGroups)
        // Extract users from userGroups
        this.members = result.userGroups.map((userGroup: any) => userGroup.user);
        this.expenses = result.expenses;
      }  
    )

    groupId && this.calculateTotalExpenses(groupId);
  }

  calculateTotalExpenses(groupId : string) {
    this.expenseService.getAllExpensesOfGroup(groupId).subscribe((expenses) => {
      this.totalExpenses = expenses.reduce((acc, expense) => acc + expense.amount, 0)
      //console.log(this.totalExpenses)
    })
  }

}
