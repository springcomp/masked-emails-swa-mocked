import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../core/auth.service';

@Component({
  selector: 'app-me',
  templateUrl: './me.component.html',
  styleUrls: ['./me.component.sass']
})
export class MeComponent implements OnInit {

  redirect: string = window.location.pathname;

  constructor(
    public authService: AuthService
  ){
  }

  async ngOnInit() {
    await this.authService.ngOnInit();
  }
}