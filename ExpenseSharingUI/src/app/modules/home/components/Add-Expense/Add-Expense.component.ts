import { AfterViewInit, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { User } from '../../../../core/models/user.model';
import { GroupService } from '../../services/group.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Group } from '../../../../core/models/group.model';
import { Expense } from '../../../../core/models/expense.model';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { ExpenseService } from '../../services/expense.service';
import { NgToastService } from 'ng-angular-popup';

interface DropdownItems{
  id:string,
  name:string
}

@Component({
  selector: 'app-Add-Expense',
  templateUrl: './Add-Expense.component.html',
  styleUrls: ['./Add-Expense.component.css']
})

export class AddExpenseComponent implements OnInit {

  paidByUser : User[] = [];
  selectedUsers: string[] = [];


  addExpenseGroup : Expense = {
    showDetails:true,
    expenseSplits:[],
    id:'',
    date:new Date(),
    amount:0,
    description:'',
    groupId:'',
    paidByUserId:'',
    splitWithUserIds:[],
    owedUser: {
      id:'',
      name:'',
      email:'',
      password:''
    },
    //owedUser : []
  }
  constructor(private groupService : GroupService, private activatedRoute : ActivatedRoute, private expenseService : ExpenseService, private toast : NgToastService, private router : Router) { 
   
  }

 

  ngOnInit() {  
    let groupId = this.activatedRoute.snapshot.paramMap.get('id')
    if (groupId) {
      this.addExpenseGroup.groupId = groupId;
      this.groupService.getGroupById(groupId).subscribe((result: Group) => {
        this.paidByUser = result.userGroups.map((userGroup: any) => userGroup.user);
        console.log('PaidByUser:', this.paidByUser);
      });
    }
    
  }

  onSelectionChange(selectedItems: string[]) {
    this.addExpenseGroup.splitWithUserIds = selectedItems;
    console.log(this.addExpenseGroup.splitWithUserIds);
  }
  getFilteredSplitWithUsers(): User[] {
    return this.paidByUser.filter(user => user.id !== this.addExpenseGroup.paidByUserId);
  }

  addExpense(){
  
    console.log('Payload:', this.addExpenseGroup);    
    this.expenseService.addExpense(this.addExpenseGroup).subscribe({
      next : (expense) => {
        console.log(expense)
        this.toast.success("Expense Added Successfully!", "SUCCESS", 5000)
        this.router.navigate(['home']);
      }
    })
  }


}
