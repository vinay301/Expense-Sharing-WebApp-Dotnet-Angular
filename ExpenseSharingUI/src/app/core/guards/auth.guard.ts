import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { AuthService } from '../../modules/auth/services/auth.service';

export const authGuard: CanActivateFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  const router = inject(Router)
  const service = inject(AuthService)
  const toast = inject(NgToastService)

  if(service.isLoggedIn())
  {
    return true;
  }
  else{
    toast.success("You must be logged in to access portal", "ERROR", 5000)
    router.navigateByUrl('home');
    return false;
  }
};
