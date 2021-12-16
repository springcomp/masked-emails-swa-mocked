import { Component } from '@angular/core';
import { AuthService } from '../app/core/auth.service'
import { ClaimsService } from './services/claims.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass']
})
export class AppComponent {
  title = 'my-app';

  public message: string | null = null;

  constructor(
    public authService: AuthService,
    public claimsService: ClaimsService
  ){
  }

  async ngOnInit() {
    await this.authService.ngOnInit();
    this.message = await this.claimsService.getClaims();
  }
}
