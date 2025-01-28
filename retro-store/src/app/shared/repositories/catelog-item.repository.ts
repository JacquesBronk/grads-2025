import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Stock } from '@shared';
import { environment } from 'src/environments/environments.local';

@Injectable({
  providedIn: 'root'
})
export class CatelogItemRepository {
  private accessToken: string | null = null;

  constructor(private _httpClient: HttpClient) {}

  // Fetch the access token automatically
  public fetchAccessToken(): Observable<void> {
    const tokenUrl = 'http://localhost:8080/realms/retro-realm/protocol/openid-connect/token';
    const body = new URLSearchParams({
      client_id: 'retro-client',
      grant_type: 'password', // Updated grant type for auto-fetch
      client_secret: 'k6LE3kUdj18kMa6eewhBWHLJTSeBPF2r', // Replace with the appropriate secret
      username: 'customerUser',
      password: 'Password1!'
    });

    return this._httpClient.post<any>(tokenUrl, body.toString(), {
      headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
    }).pipe(
      tap((response) => {
        this.accessToken = response.access_token;
      }),
      catchError((error) => {
        console.error('Error fetching access token:', error);
        return throwError(() => error);
      })
    );
  }

  // Get all items with the access token
  public getAllItems(): Observable<Stock> {
    if (!this.accessToken) {
      console.error('Access token is not set.');
      return throwError(() => new Error('Access token is not set.'));
    }

    const apiUrl = `${environment.stockUrl}pageNumber=1&pageSize=20`;

    // Add the Bearer token to the headers
    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.accessToken}`
    });

    return this._httpClient.get<Stock>(apiUrl, { headers }).pipe(
      catchError((error) => {
        console.error('Error fetching catalog items:', error);
        return throwError(() => error);
      })
    );
  }
}
