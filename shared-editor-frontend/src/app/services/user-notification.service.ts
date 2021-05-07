import { Injectable } from '@angular/core';
import { ConnectionService } from './connection.service';
import { filter, map } from 'rxjs/operators';
import { BehaviorSubject, pipe, Subject } from 'rxjs';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';

type UserJoined = 'USER_JOINED';
type UserLeft = 'USER_LEFT';
type UserAction = UserJoined | UserLeft;

const USER_JOINED: UserJoined = 'USER_JOINED';
const USER_LEFT: UserLeft = 'USER_LEFT';

const isUserJoined = (action: UserAction) => action == USER_JOINED;
const isUserLeft = (action: UserAction) => action == USER_LEFT;
const notNull = <T>(param: T | null | undefined) => param != null && param != undefined;
const toNotNull = <T>(param: T | null | undefined) => param as T;

const takeIfDefined = pipe(filter(notNull));
const unwrap = pipe(map(toNotNull));

@Injectable({
  providedIn: 'root'
})
export class UserNotificationService {
  private userActionSubject = new Subject<UserAction>();
  userJoined = this.userActionSubject.asObservable().pipe(filter(isUserJoined));
  userLeft = this.userActionSubject.asObservable().pipe(filter(isUserLeft));

  constructor(private connectionService: ConnectionService) {
    this.subscribeToConnection();
  }

  connect() {
    this.connectionService.connectTo('/user-notification');
  }

  private subscribeToConnection() {
    this.connectionService.connection
      .pipe(
        takeIfDefined,
        unwrap
      )
      .subscribe(conn => {
        conn.on(USER_JOINED, () => this.userActionSubject.next(USER_JOINED));
        conn.on(USER_LEFT, () => this.userActionSubject.next(USER_LEFT));
      });
  }
}
