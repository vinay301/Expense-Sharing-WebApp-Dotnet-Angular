import { Component, OnInit } from '@angular/core';
import { Group } from '../../../../core/models/group.model';
import { GroupService } from '../../services/group.service';
import { ActivatedRoute } from '@angular/router';
import { User } from '../../../../core/models/user.model';
import { Expense } from '../../../../core/models/expense.model';

@Component({
  selector: 'app-View-Group',
  templateUrl: './View-Group.component.html',
  styleUrls: ['./View-Group.component.css']
})
export class ViewGroupComponent implements OnInit {

  groupDetails !: Group;
  members: User[] = [];
  expenses: Expense[] = [];
  constructor(private groupService : GroupService, private activeRoute : ActivatedRoute) { }

  ngOnInit() {
    let groupId = this.activeRoute.snapshot.paramMap.get('id');
    //console.warn(groupId);
    groupId && this.groupService.getGroupById(groupId).subscribe(
      (result : Group) => {
        this.groupDetails = result;
        console.warn(result)
        console.log("members:", result.userGroups.$values)
        // Extract users from userGroups
        this.members = result.userGroups.$values.map((userGroup: any) => userGroup.user);
        // this.expenses = res.expenses.filter(expense => typeof expense === 'object') // Filter out strings
        //             .map(expense => expense as Expense);
        this.expenses = result.expenses.$values;

        // this.expenses.push({
        //   id: firstExpense.id,
        //   description: firstExpense.description,
        //   amount: firstExpense.amount,
        //   date: firstExpense.date
        // });
         
        // console.log("Extracted expenses:", this.expenses);
      }  
    )
  }

}
