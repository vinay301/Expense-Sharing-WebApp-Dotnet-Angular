import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { NgToastService } from 'ng-angular-popup';
import { Route, Router } from '@angular/router';
import { UserTokenService } from '../services/user-token.service';
import ValidateForm from '../../../core/helpers/ValidateForm';
import { Toast, ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private fb:FormBuilder, private authService:AuthService, private toast: NgToastService, private router:Router, private userStore: UserTokenService ) { }
  loginForm !: FormGroup;
  showRegisterForm = false;
  toggleForm() {
    this.showRegisterForm = !this.showRegisterForm;
  }
  ngOnInit() {
    console.log(this.loginForm)
    this.loginForm = this.fb.group({
      username : ['',Validators.required],
      password : ['',Validators.required]
    })
  }

  onLogin(){
    if(this.loginForm.valid)
      {
        console.log(this.loginForm.value);
        this.authService.login(this.loginForm.value).subscribe({
          next : (res => {
            const token = res.result.token;
            this.loginForm.reset();
            this.authService.storeToken(token);
            const tokenPayload = this.authService.decryptToken();
            this.userStore.setUsernameForStore(tokenPayload.unique_name);
            this.userStore.setRoleForStore(tokenPayload.role);
            res.message = "Logged in Successfully";
            //console.log(res);
            this.toast.success(res.message, "SUCCESS", 5000)
            //this.toast.success({detail:"SUCCESS",summary:res.result.user.name + " " + res.message,duration:5000});
            this.router.navigate(['home']);
          }),
          error : (err => {
            // console.log(err);
            // console.log(err?.error?.message);
            // alert(err?.error.message)
            this.toast.success(err.message);
            //this.toast.error({detail:"ERROR",summary:err?.error?.message,sticky:true,duration:5000});
          }),
        })
      }else{
        console.log("Form is not valid");
        //Throw error using toastr and with required fields
        ValidateForm.validateAllFormFields(this.loginForm);
        //alert("Your Form is invalid");
        this.toast.danger("ERROR","Your Form is Invalid",5000);
      }
  }

}
