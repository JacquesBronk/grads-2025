import { Injectable } from '@angular/core';
import { CatelogItemRepository, Stock } from '@shared';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root',
})
export class CatelogService {

  constructor(private _catelogItemRepo: CatelogItemRepository) { }

  public getAllItems(): Observable<Stock>{
    return this._catelogItemRepo.getAllItems();
  }
}
