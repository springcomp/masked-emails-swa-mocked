import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ClaimsService {

  constructor() { }

  public async getClaims(): Promise<string> {
    const response = await fetch("/api/claims");
    const payload = await response.json();
    return JSON.stringify(payload);
  }
}
