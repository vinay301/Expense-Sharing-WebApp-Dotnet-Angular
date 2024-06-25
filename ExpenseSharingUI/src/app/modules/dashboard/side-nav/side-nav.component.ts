import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-side-nav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.css']
})
export class SideNavComponent implements OnInit {

  @Input() sideNavStatus : boolean = false;
  sideNavList = [
    {
      number:1,
      icon:'home',
      title:'Home',
      link:'/home'
    },
    {
      number:2,
      icon:'group_add',
      title:'Add Groups',
      link:'/add-groups'
    }
  ]
  constructor() { }

  ngOnInit() {
  }

}
