import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../../core/services/user.service';
import { User } from '../../../../core/models/user.model';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { Group } from '../../../../core/models/group.model';
import { GroupService } from '../../services/group.service';
import { NgToastService } from 'ng-angular-popup';
import { Router } from '@angular/router';

@Component({
  selector: 'app-Add-Groups',
  templateUrl: './Add-Groups.component.html',
  styleUrls: ['./Add-Groups.component.css']
})
export class AddGroupsComponent implements OnInit {

  users : User[] = [];
  selectedUsers: User[] = [];

  dropdownList = [];
  selectedItems = [];
  dropdownSettings : IDropdownSettings = {};

  addGroupRequest : Group = {
    id:'',
    name:'',
    description:'',
    createdDate: new Date(),
    memberIds:[],
    userGroups:[],
    expenses:[]
  }

  showError = false;
  constructor(private userService : UserService, private groupService:GroupService, private toast : NgToastService, private router : Router) { }

  ngOnInit() {
    this.dropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'name',
      selectAllText: 'Select All',
      unSelectAllText: 'Unselect All',
      itemsShowLimit: 3,
      allowSearchFilter: true
    };

    this.userService.getAllUsers().subscribe({
      next : (users) => {
        this.users = users;
        console.log(users)
      }
    })
  }

  onDropdownClick() {
    this.showError = true;
  }
  onItemSelect(item: any) {
    this.addGroupRequest.memberIds.push(item.id);
    this.showError = false;
    console.log(this.addGroupRequest.memberIds);
  }

  onSelectAll(items: any) {
    this.addGroupRequest.memberIds = items.map((item: User) => item.id);
    this.showError = false;
    console.log(this.addGroupRequest.memberIds);
  }

  onItemDeselect(item: any) {
    this.addGroupRequest.memberIds = this.addGroupRequest.memberIds.filter(id => id !== item.id);
    if(this.selectedUsers.length == 0) {
      this.showError = true;
    }
    console.log(this.addGroupRequest.memberIds);
  }

  onDeselectAll(items:any) {
    this.addGroupRequest.memberIds = [];
    this.showError = true;
    console.log(this.addGroupRequest.memberIds);
  }


  createGroup(){
    this.groupService.addGroup(this.addGroupRequest).subscribe({
      next : (group) => {
        console.log(group)
        this.toast.success("Group Created Successfully!", "SUCCESS", 5000)
        this.router.navigate(['home']);
      }
    })
  }

}
