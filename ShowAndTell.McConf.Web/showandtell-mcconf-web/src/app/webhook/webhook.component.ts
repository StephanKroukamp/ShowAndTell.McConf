import { Component } from '@angular/core';
import { WebhookService } from '../webhook.service'

@Component({
  selector: 'app-webhook',
  template: `
    <p *ngIf="webhookResponse">{{ webhookResponse }}</p>
  `
})
export class WebhookComponent {
  public webhookResponse: string | undefined;

  constructor(private webhookService: WebhookService) {
    this.webhookService.listenForWebhook().subscribe(response => {
      this.webhookResponse = response.message;
      console.log(`Received webhook with message: ${this.webhookResponse}`);
    });
  }
}