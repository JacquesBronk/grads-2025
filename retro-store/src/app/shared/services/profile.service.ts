import { Injectable } from '@angular/core';
import { Profile, ProfileRepository } from '@shared';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  constructor(private _profileRepository: ProfileRepository) { }

    public getProfile(): Observable<Profile>{
      return this._profileRepository.getProfile();
    }
}
