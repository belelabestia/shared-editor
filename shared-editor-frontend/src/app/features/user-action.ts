import { InjectionToken } from '@angular/core';
import { Subject } from 'rxjs';

export enum UserAction {
  Joined,
  Left
}

export const UserActionSubject = new InjectionToken<Subject<UserAction>>('UserActionSubject');
