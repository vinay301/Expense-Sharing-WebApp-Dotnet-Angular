import { Component, EventEmitter, OnInit, Output, inject } from '@angular/core';
import { AuthService } from '../../auth/services/auth.service';
import { NgToastService } from 'ng-angular-popup';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  @Output() sideNavToggled = new EventEmitter<boolean>(); 
  menuStatus : boolean = false; //by default side nav is closed

  isLoggedIn = false;
  username: string = ""
  userId:string = ""
  private = inject(NgToastService);
  constructor(private authService : AuthService,  private toast:NgToastService, private router : Router) { }

  ngOnInit() {
    this.isLoggedIn = this.authService.isLoggedIn();
    this.username = this.authService.getUsernameFromToken();
   
    this.userId = this.authService.getUserIdFromToken();

    // console.log(this.isLoggedIn)
    // console.log(this.username)
  }

  sideNavToggle(){
    this.menuStatus = !this.menuStatus
    this.sideNavToggled.emit(this.menuStatus);
  }

  logout(){
    this.authService.signOut();
    this.toast.success("Logout Successfully", "SUCCESS", 5000)
    this.router.navigate(['login']);
    // window.location.reload();
  }
}
