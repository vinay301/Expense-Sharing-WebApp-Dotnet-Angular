import { Component, OnInit, inject } from '@angular/core';
import { Group } from '../../../../core/models/group.model';
import { GroupService } from '../../services/group.service';
import { ActivatedRoute } from '@angular/router';
import { User } from '../../../../core/models/user.model';
import { Expense } from '../../../../core/models/expense.model';
import { ExpenseService } from '../../services/expense.service';
import { AuthService } from '../../../auth/services/auth.service';
import { Location } from '@angular/common';

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

  balances: { [key: string]: number } = {};
  loggedInUserId : string = '';
  isAdmin : boolean = false;
  userIds: string[] = [];
  currentImage: string = '';
  location = inject(Location);

  randomExpenseImages = [
   'https://img.freepik.com/free-vector/bill-analysis-concept-illustration_114360-19348.jpg?t=st=1719413624~exp=1719417224~hmac=123e6b963c2bbc7cb56646dc8718bb58f5879438752aa38e70cbe7e7066f2e06&w=740',
   'https://img.freepik.com/free-vector/manage-money-concept-illustration_114360-8079.jpg?t=st=1719413659~exp=1719417259~hmac=18b0c8e81b1f5d326eff38f708154343a1120e8bc1b0785b4ddde31b74f9537b&w=740',
   'https://img.freepik.com/free-vector/e-wallet-concept-illustration_114360-7561.jpg?t=st=1719413119~exp=1719416719~hmac=47cb4b27334684b18ef56a9b075905632ffd20f6f1982e356fbc77b5d6526c81&w=740',
   'https://img.freepik.com/free-vector/money-stress-concept-illustration_114360-8907.jpg?t=st=1719413749~exp=1719417349~hmac=882204d47a8ad137209aa711944b9f1fe8f2b9d6b53f8ca40009a518c74f3b4e&w=740'
  ];
  constructor(private groupService : GroupService, private activeRoute : ActivatedRoute, private expenseService : ExpenseService, private authService : AuthService) { }

  ngOnInit() {
    this.loggedInUserId = this.authService.getUserIdFromToken();
    let groupId = this.activeRoute.snapshot.paramMap.get('id');
    
    groupId && this.loadGroupBalance(groupId);
    if(groupId){this.currentImage = this.getRandomExpenseImage(groupId);}
    //console.warn(groupId);
    groupId && this.groupService.getGroupById(groupId).subscribe(
      (result : Group) => {
        this.groupDetails = result;
        this.isAdmin = result.admins.some(admin => admin.id === this.loggedInUserId);
        console.log(this.isAdmin)
        // console.warn(result)
        // console.log("members:", result.userGroups)
        // Extract users from userGroups
        this.members = result.userGroups.map((userGroup: any) => userGroup.user);
        //this.expenses = result.expenses;
        //console.warn(result)
      }  
    )
    groupId && this.expenseService.getAllExpensesOfGroup(groupId).subscribe(
      (result : Expense[]) => {
        this.expenses = result;
        console.log(this.expenses)
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

  loadGroupBalance(groupId:string){
    this.expenseService.getGroupBalanceByGroupId(groupId).subscribe((res) => {
      this.balances = res;
      //console.log("GroupBalance:",this.balances);
      this.userIds = Object.keys(this.balances);
      //console.log("User IDs:", this.userIds);
    })
  }

  getUserIds(): string[] {
    //return Object.keys(this.balances);
    return this.userIds.filter(id => id == this.loggedInUserId);
    //return Object.keys(this.balances).filter(id => id === this.loggedInUserId);
  }

  // getRandomExpenseImage(): string {
  //   const randomIndex = Math.floor(Math.random() * this.randomExpenseImages.length);
  //   return this.randomExpenseImages[randomIndex];
  // }

  getRandomExpenseImage(groupId: string): string {
    const index = this.hashStringToIndex(groupId, this.randomExpenseImages.length);
    return this.randomExpenseImages[index];
  }

  // Simple hash function to generate an index from a string
  hashStringToIndex(str: string, max: number): number {
    let hash = 0;
    for (let i = 0; i < str.length; i++) {
      hash = (hash << 5) - hash + str.charCodeAt(i);
      hash |= 0; // Convert to 32bit integer
    }
    return Math.abs(hash % max);
  }


  back(){
    this.location.back();
  }

}
