import { Injectable } from '@angular/core';
import { Router, Route, CanActivate, CanLoad, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { from, Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { AuthService } from './auth.service';

@Injectable()
export class AuthorizationGuard implements CanActivate, CanLoad {

  constructor(
    private router: Router,
    private authService: AuthService
  ) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    return this.checkUser(route.url[0].path);
  }

  canLoad(state: Route): Observable<boolean> {
    return this.checkUser();
  }

  private checkUser(route?: string): Observable<boolean> {
    return of(true);
  }
}
