import { Component } from '@angular/core';
import { ConnectionService } from './services/connection.service';
import { UserNotificationService } from './services/user-notification.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(
    private connectionService: ConnectionService,
    private userNotificationService: UserNotificationService
  ) {
    this.registerEventLoggers();
    this.connectServices();
  }

  private registerEventLoggers(): void {
    this.userNotificationService.userJoined.subscribe(() => console.log('yaay!'));
    this.userNotificationService.userLeft.subscribe(() => console.log('uff'));
  }

  private connectServices(): void {
    this.connectionService.connect().then(() => console.log('state hub connected'));
    this.userNotificationService.connect();
  }
}
