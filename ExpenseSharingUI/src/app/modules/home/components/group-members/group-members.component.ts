import { Component, OnInit } from '@angular/core';
import { Group } from '../../../../core/models/group.model';
import { User } from '../../../../core/models/user.model';
import { ActivatedRoute } from '@angular/router';
import { GroupService } from '../../services/group.service';

@Component({
  selector: 'app-group-members',
  templateUrl: './group-members.component.html',
  styleUrls: ['./group-members.component.css']
})
export class GroupMembersComponent implements OnInit {

  groupDetails !: Group;
  members: User[] = [];
  constructor(private activatedRoute : ActivatedRoute, private groupService:GroupService) { }

  ngOnInit() {
    let groupId = this.activatedRoute.snapshot.paramMap.get('id');
    groupId && this.groupService.getGroupById(groupId).subscribe(
      (res : Group) => {
        this.groupDetails = res;
        this.members = res.userGroups.map((userGroup: any) => userGroup.user);
      }
    )
  }

}
