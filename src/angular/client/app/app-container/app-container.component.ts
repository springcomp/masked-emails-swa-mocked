import { AuthService } from '../core/auth.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { Profile } from '../shared/models/model';
import { ProfileService, Claim } from '../shared/services/profile.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-container',
  templateUrl: './app-container.component.html',
  styleUrls: ['./app-container.component.scss']
})
export class AppContainerComponent implements OnInit {
  @ViewChild('sidenav', { static: false }) public sidenav: MatSidenav;

  public isAuthenticated: boolean = false;

  public my: Profile | undefined = undefined;
  public claims: Claim[] = [];

  constructor(
    private profileService: ProfileService,
    public authService: AuthService,
    public router: Router
  ) {
  }

  async ngOnInit(){
    await this.authService.ngOnInit();
    if (this.authService.getIsAuthorized()){
        this.isAuthenticated = true;
        this.loadProfile();
    }
    this.router.events.subscribe(event => {
      // close side nav on routing
      if (this.sidenav && this.sidenav.opened) {
        this.sidenav.close();
      }
    });
  }

  get forwardingAddress(): string {
    return this.my && this.my.forwardingAddress ? this.my.forwardingAddress : '';
  }

  get userIsAuthenticated(): boolean {
    return this.isAuthenticated && this.my != undefined;
  }

  public login() {
    this.authService.login();
  }

  public logout() {
    this.authService.logout();
  }

  public home() {
    this.router.navigate(['/masked-emails']);
  }

  public inbox() {
    this.router.navigate(['/inbox']);
  }

  public updateUserModel(user: Profile) {
    this.my = user;
  }

  private loadProfile(): void {
    this.profileService.getProfile()
      .subscribe(profile => this.my = profile);
    this.profileService.getClaims()
      .subscribe(claims => this.claims = claims);
  }
}
