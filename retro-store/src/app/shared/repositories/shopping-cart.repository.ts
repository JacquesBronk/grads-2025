import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CatelogItem } from '@shared';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environments.local';

@Injectable({
  providedIn: 'root'
})
export class ShoppingCartRepository {

  constructor(private _httpClient: HttpClient) { }

  public getShoppingCart(): Observable<CatelogItem>{
    const apiUrl = environment.cartUrl;
    return this._httpClient.get<CatelogItem>(apiUrl + 'cart-api/');
  }
}
