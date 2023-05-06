import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WebhookService {
  private readonly apiUrl = 'http://localhost:63873'; // Update with the URL for your backend

  constructor(private http: HttpClient) {}

  public listenForWebhook(): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/api/Prompts/GenerateMessage/`, {});
  }
}