import { DOCUMENT } from '@angular/common';
import { Injectable, OnInit, Inject } from '@angular/core';
import { UserInfo } from '../shared/models/userInfo';

@Injectable({
  providedIn: 'root'
})
export class AuthService implements OnInit {

  public clientPrincipal: UserInfo | undefined;

  constructor(
    @Inject(DOCUMENT) private document: Document
  ) { }

  async ngOnInit(){
    this.clientPrincipal = await this.getClientPrincipal();
  }

  public getIsAuthenticated(): boolean {
    return true;
  }
  public getIsAuthorized(): boolean {
    return true;
  }
  public getIsInRole(roleName: string): boolean {
    return true;
  }

  public navigate(url: string): void {
    this.document.location.href = url;
  }

  public login(): void{
    const url = '/masked-emails';
    this.navigate(url);
  }
  
  public logout(): void {
    const url = '/home';
    this.navigate(url);
  }

  private async getClientPrincipal() {
    const principal: UserInfo = {
      identityProvider: 'aad',
      userId: '3a15a264-cf52-4d51-9df6-d27d28301c71',
      userDetails: 'Alice',
      userRoles: ['anonymous', 'authenticated', 'registered']
    };
    return principal;
  }
}
