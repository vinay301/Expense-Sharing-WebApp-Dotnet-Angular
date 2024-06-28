import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './modules/auth/login/login.component';
import { GroupsComponent } from './modules/home/components/groups/groups.component';
import { DashboardComponent } from './modules/dashboard/dashboard/dashboard.component';
import { AddGroupsComponent } from './modules/home/components/Add-Groups/Add-Groups.component';
import { ViewGroupComponent } from './modules/home/components/View-Group/View-Group.component';
import { AllExpensesComponent } from './modules/home/components/All-Expenses/All-Expenses.component';
import { AddExpenseComponent } from './modules/home/components/Add-Expense/Add-Expense.component';
import { GroupMembersComponent } from './modules/home/components/group-members/group-members.component';
import { authGuard } from './core/guards/auth.guard';
import { ExpenseDetailsComponent } from './modules/home/components/expense-details/expense-details.component';



const routes: Routes = [
  {path:'',redirectTo: 'login', pathMatch:"full"},
  {path:'login',component:LoginComponent},
  {path:'',component:DashboardComponent, children:[
    {path:'add-groups',component:AddGroupsComponent},
    {path:'home',component:GroupsComponent},
    {path:'view-group/:id',component:ViewGroupComponent},
    {path:'all-expenses/:id',component:AllExpensesComponent},
    {path:'add-expense/:id',component:AddExpenseComponent},
    {path:'members/:id', component:GroupMembersComponent},
    {path:'expense-detail/:id',component:ExpenseDetailsComponent}
],
canActivate:[authGuard]
},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
