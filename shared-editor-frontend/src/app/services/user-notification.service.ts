import { Injectable } from '@angular/core';
import { ConnectionService } from './connection.service';
import { filter } from 'rxjs/operators';
import { Subject } from 'rxjs';

type UserJoined = 'USER_JOINED';
type UserLeft = 'USER_LEFT';
type UserAction = UserJoined | UserLeft;

const USER_JOINED: UserJoined = 'USER_JOINED';
const USER_LEFT: UserLeft = 'USER_LEFT';

const isUserJoined = (action: UserAction) => action == USER_JOINED;
const isUserLeft = (action: UserAction) => action == USER_LEFT;

@Injectable({
  providedIn: 'root'
})
export class UserNotificationService {
  private userActionSubject = new Subject<UserAction>();
  userJoined = this.userActionSubject.asObservable().pipe(filter(isUserJoined));
  userLeft = this.userActionSubject.asObservable().pipe(filter(isUserLeft));

  constructor(private connectionService: ConnectionService) { }

  connect(): void {
    this.registerMethods();
  }

  disconnect(): void {
    this.unregisterMethods();
  }

  private registerMethods(): void {
    this.connectionService.connection.on(USER_JOINED, () => this.userActionSubject.next(USER_JOINED));
    this.connectionService.connection.on(USER_LEFT, () => this.userActionSubject.next(USER_LEFT));
  }

  private unregisterMethods(): void {
    this.connectionService.connection.off(USER_JOINED);
    this.connectionService.connection.off(USER_LEFT);
  }
}
