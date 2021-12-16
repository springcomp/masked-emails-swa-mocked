import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { MeComponent } from './components/me/me.component';
import { Err404Component } from './components/err404/err404.component';
import { HomeComponent } from './components/home/home.component';
import { AuthorizationGuard } from './core/authorizationGuard';

@NgModule({
  declarations: [
    AppComponent,
    MeComponent,
    Err404Component,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [
    AuthorizationGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
