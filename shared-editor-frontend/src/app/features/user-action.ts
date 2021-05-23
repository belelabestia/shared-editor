import { InjectionToken, Pipe, PipeTransform } from '@angular/core';
import { Subject } from 'rxjs';

export enum UserAction {
  Joined,
  Left
}

export const UserActionSubject = new InjectionToken<Subject<UserAction>>('UserActionSubject');

@Pipe({
  name: 'userAction',
  pure: true
})
export class UserActionPipe implements PipeTransform {
  transform(value: UserAction | null): string {
    switch (value) {
      case UserAction.Joined: return 'User joined';
      case UserAction.Left: return 'User left';
      case null: return 'None';
      default: throw new Error('Invalid UserAction');
    }
  }
}