import { Component, OnInit } from '@angular/core';
import { GroupService } from '../../services/group.service';
import { Group } from '../../../../core/models/group.model';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.css']
})
export class GroupsComponent implements OnInit {

  groups : Group[] = [];
  constructor(private groupService : GroupService) { }

  ngOnInit() {
    this.groupService.getAllGroups().subscribe({
      next: (groups) => {
        this.groups = groups;
        console.log(groups);
      },
      error : (res) => {
        console.log(res);
      }
    })
  }

}
