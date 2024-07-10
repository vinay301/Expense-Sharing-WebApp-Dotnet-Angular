import { Component, OnInit, inject } from '@angular/core';
import { Group } from '../../../../core/models/group.model';
import { User } from '../../../../core/models/user.model';
import { ActivatedRoute } from '@angular/router';
import { GroupService } from '../../services/group.service';
import { UserService } from '../../../../core/services/user.service';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { NgToastComponent, NgToastService } from 'ng-angular-popup';
import { Location } from '@angular/common';
import { AuthService } from '../../../auth/services/auth.service';

@Component({
  selector: 'app-group-members',
  templateUrl: './group-members.component.html',
  styleUrls: ['./group-members.component.css']
})
export class GroupMembersComponent implements OnInit {

  groupDetails !: Group;
  members: User[] = [];

  addMemberList : User[] = [];
  dropdownList = [];
  selectedItems : User[] = [];
  dropdownSettings : IDropdownSettings = {};
  selectedIds: string[] = [];

  location = inject(Location)
  loggedInUserId : string = '';
  isAdmin : boolean = false;
 
  constructor(private activatedRoute : ActivatedRoute, private groupService:GroupService, private userService : UserService, private toast : NgToastService, private authService : AuthService) { }

  ngOnInit() {

    this.loggedInUserId = this.authService.getUserIdFromToken();
    console.log(this.loggedInUserId)
    this.dropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'name',
      selectAllText: 'Select All',
      unSelectAllText: 'Unselect All',
      itemsShowLimit: 3,
      allowSearchFilter: true
    };

    let groupId = this.activatedRoute.snapshot.paramMap.get('id');
    groupId && this.groupService.getGroupById(groupId).subscribe(
      (res : Group) => {
        this.groupDetails = res;
        this.members = res.userGroups.map((userGroup: any) => userGroup.user);
        this.loadAddMembersDropDownList();
        // Check if logged-in user is admin
        this.isAdmin = res.admins.some(admin => admin.id === this.loggedInUserId);
       console.log(this.isAdmin)
      }
    )
  }

  addMembers(groupId : string){
    if(groupId){
      // const userIds = Array.from(new Set(this.selectedItems.map(item => String(item.id))));
      // console.log("UserIds",userIds);
      const userIds = this.selectedItems.map(item => item.id);
      console.log(userIds)
      this.groupService.addMembersInGroup(groupId, userIds).subscribe(
        (res) => {
          console.log(res);
          this.toast.success("Success","Members added successfully",3000)
          setTimeout(()=>{
            window.location.reload();
          },3000)
         
        }
      )
    }
  }

  loadAddMembersDropDownList(){
    this.userService.getAllUsers().subscribe(
      (res) => {
        const memberIds = this.members.map(member => member.id);
        this.addMemberList = res.filter(user => !memberIds.includes(user.id));
        console.log(this.addMemberList.map(u => u.name));
      }
    )
  }



  onItemSelect(item: any) {
    const id = item.id; 
    if (id) {
      this.selectedIds.push(id);
    }
    console.log("Selected IDs:", this.selectedIds);
  }
  onSelectAll(items: any) {
    const ids = items.map((item: any) => item.id).filter((id: string) => id); 
    this.selectedIds.push(...ids);
    console.log("Selected IDs:", this.selectedIds);
  }

  deleteMemberOfGroup(groupId:string, userId:string){
    this.groupService.deleteMemberOfGroup(groupId, userId).subscribe(
      ()=>{
        this.toast.success("Success","Member deleted successfully",3000);
        setTimeout(()=>{
          window.location.reload();
        }
        ,1000)
      }
    );
  }

  
  back(){
    this.location.back();
  }
  
}
