import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseApiUrl : string = environment.baseApiUrl;
  private userPayload : any;
  constructor(private http : HttpClient, private router : Router) {
    this.userPayload = this.decryptToken();
   }

   register(userObj : any){
    return this.http.post<any>(this.baseApiUrl + '/api/auth/Register',userObj);
  }
  
  login(loginObj  :any){
    return this.http.post<any>(this.baseApiUrl + '/api/auth/Login',loginObj);
  }
  
  //To store JWT Token
  storeToken(tokenValue : string){
    localStorage.setItem('expense_token',tokenValue);
  }
  getToken(){
    return localStorage.getItem('expense_token');
  }
  
  //To store RefreshToken
  storeRefreshToken(tokenValue : string){
    localStorage.setItem('refreshToken',tokenValue);
  }
  getRefreshToken(){
    return localStorage.getItem('refreshToken');
  }
  
  isLoggedIn() : boolean {
    //it returns a string but (!!) it converts into boolean
    return !! localStorage.getItem('expense_token');
  }
  
  signOut()
  {
    localStorage.clear();
    this.router.navigate(['']);
  }
  
  decryptToken(){
    const jwtHelper = new JwtHelperService
    const token = this.getToken()!;
    return jwtHelper.decodeToken(token)
  }
  
  getUsernameFromToken(){
    if(this.userPayload)
    {
      return this.userPayload.name;
    }
  }
  getUserIdFromToken(){
    if(this.userPayload)
    {
      return this.userPayload.sub;
    }
  }
  getRoleFromToken(){
    if(this.userPayload)
    {
      return this.userPayload.role;
    }
  }

}
