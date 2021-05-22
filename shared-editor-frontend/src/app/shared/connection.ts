import { InjectionToken, Provider } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';

export const UserActionConnection = new InjectionToken<HubConnection>('UserActionConnection');
export const EditorConnection = new InjectionToken<HubConnection>('EditorConnection');

const builder = new HubConnectionBuilder();

export const connectionProvider = <T>(token: InjectionToken<T>, endpoint: string): Provider => ({
  provide: token,
  useValue: builder.withUrl(endpoint).build()
});
