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
    connectionService: ConnectionService,
    userNotificationService: UserNotificationService
  ) {
    connectionService.connection.subscribe(console.log);
    connectionService.connectTo('/connection-state');

    userNotificationService.userJoined.subscribe(() => console.log('yaay!'));
    userNotificationService.userLeft.subscribe(() => console.log('uff'));
  }
}
