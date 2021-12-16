import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Err404Component } from './components/err404/err404.component';
import { HomeComponent } from './components/home/home.component';
import { MeComponent } from './components/me/me.component';
import { AuthorizationGuard } from './core/authorizationGuard';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: '/home' },
  { path: 'home', component: HomeComponent },
  { path: 'me', component: MeComponent, canActivate: [AuthorizationGuard] },
  { path: '404', component: Err404Component },
  { path: '**', redirectTo: '/404' }
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
