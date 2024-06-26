import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './modules/auth/login/login.component';
import { HttpClientModule } from '@angular/common/http';
import { GroupsComponent } from './modules/home/components/groups/groups.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import {MatToolbarModule} from '@angular/material/toolbar';
import { DashboardComponent } from './modules/dashboard/dashboard/dashboard.component';
import { HeaderComponent } from './modules/dashboard/header/header.component';
import { SideNavComponent } from './modules/dashboard/side-nav/side-nav.component';
import {MatIconModule} from '@angular/material/icon'
import { AddGroupsComponent } from './modules/home/components/Add-Groups/Add-Groups.component';
import { ViewGroupComponent } from './modules/home/components/View-Group/View-Group.component';
import { AllExpensesComponent } from './modules/home/components/All-Expenses/All-Expenses.component';
import { AddExpenseComponent } from './modules/home/components/Add-Expense/Add-Expense.component';
import { GroupMembersComponent } from './modules/home/components/group-members/group-members.component';
import { NgToastModule } from 'ng-angular-popup';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastrModule, provideToastr } from 'ngx-toastr';
import { BrowserAnimationsModule, provideAnimations } from '@angular/platform-browser/animations';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { CustomMultiSelectComponent } from './shared/components/custom-multi-select/custom-multi-select.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    GroupsComponent,
    DashboardComponent,
    HeaderComponent,
    SideNavComponent,
    AddGroupsComponent,
    ViewGroupComponent,
    AllExpensesComponent,
    AddExpenseComponent,
    GroupMembersComponent,
    CustomMultiSelectComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    MatToolbarModule,
    MatIconModule,
    NgToastModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule, // required animations module
    ToastrModule.forRoot(), // ToastrModule added
    NgMultiSelectDropDownModule.forRoot()
  ],
  providers: [
    provideAnimationsAsync(),
    provideAnimations(), // required animations providers
    provideToastr(), // Toastr providers
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
