import { Injectable } from '@angular/core';
import { CatelogItem, ShoppingCartRepository } from '@shared';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ShoppingCartService {

  constructor(private _shoppingCartRepository: ShoppingCartRepository) { }

      public getProfile(): Observable<CatelogItem>{
        return this._shoppingCartRepository.getShoppingCart();
      }
}
