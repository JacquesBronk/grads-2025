import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Profile } from '@shared';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environments.local';

@Injectable({
  providedIn: 'root'
})
export class ProfileRepository {

  constructor(private _httpClient: HttpClient) { }

  public getProfile(): Observable<Profile>{
    const apiUrl = environment.profileUrl;
    return this._httpClient.get<Profile>(apiUrl + 'profile-api/');
  }




}
