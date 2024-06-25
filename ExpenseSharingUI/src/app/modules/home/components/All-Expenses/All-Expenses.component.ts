import { Component, OnInit } from '@angular/core';
import { GroupService } from '../../services/group.service';
import { ActivatedRoute } from '@angular/router';
import { Group } from '../../../../core/models/group.model';
import { Expense } from '../../../../core/models/expense.model';

@Component({
  selector: 'app-All-Expenses',
  templateUrl: './All-Expenses.component.html',
  styleUrls: ['./All-Expenses.component.css']
})
export class AllExpensesComponent implements OnInit {

  groupDetails !: Group;
  expenses: Expense[] = [];
  constructor(private groupService : GroupService, private activeRoute : ActivatedRoute) { }

  ngOnInit() {
    let groupId = this.activeRoute.snapshot.paramMap.get('id');
    groupId && this.groupService.getGroupById(groupId).subscribe(
      (res : Group) => {
        this.groupDetails = res;
        this.expenses = res.expenses;
        console.log(this.expenses)
      }
    )
  }

}
