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
  currentImage: string = '';
  randomGroupImages = [
    'https://img.freepik.com/free-vector/brand-loyalty-concept-illustration_114360-15542.jpg?t=st=1719415701~exp=1719419301~hmac=8220061b750a7192d4cc2e7e194868f94c456dece44169170f5232a191269932&w=740',
    'https://img.freepik.com/free-vector/grades-concept-illustration_114360-618.jpg?t=st=1719415720~exp=1719419320~hmac=26c725f597a4139bfcf0a607c68c7213b864463d0f0460f25c48b3fce7bf8e72&w=740',
    'https://img.freepik.com/free-vector/selfie-concept-illustration_114360-570.jpg?t=st=1719415744~exp=1719419344~hmac=c2e44a014fe1ca281e411104d9c0851d5f5b0ff0898cb2d897dc0478524312d1&w=740',
    'https://img.freepik.com/free-vector/group-video-concept-illustration_114360-4942.jpg?t=st=1719415774~exp=1719419374~hmac=77cb8124f4fa91cfa8bb68c0cbedbb4f7578e22695e235c6fa4a452297c02b6d&w=740',
    'https://img.freepik.com/free-vector/brand-loyalty-concept-illustration_114360-11553.jpg?t=st=1719415803~exp=1719419403~hmac=d40fbb33fc90d37f46c85ec849c1f7f891dccdb0959cc1bc51223035e01b4dda&w=740'
  ];
  constructor(private groupService : GroupService) { }

  ngOnInit() {
    this.groupService.getAllGroups().subscribe({
      next: (groups) => {
        this.groups = groups;
        //this.currentImage = this.getRandomGroupImages();
        console.log(groups);
      },
      error : (res) => {
        console.log(res);
      }
    })
  }

  getRandomGroupImage(groupId: string): string {
    const index = this.hashStringToIndex(groupId, this.randomGroupImages.length);
    return this.randomGroupImages[index];
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
  getAdminNames(group: Group): string {
    if (group.admins && group.admins.length > 0) {
      return group.admins.map(admin => admin.name).join(', ');
    }
    return '';
  }

}
