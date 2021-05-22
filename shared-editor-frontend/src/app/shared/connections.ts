import { InjectionToken } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';

const builder = new HubConnectionBuilder();
const connectionFactory = (endpoint: string): HubConnection => builder.withUrl(endpoint).build();

const connectionProvider = (endpoint: string) => ({
  providedIn: 'root' as 'root',
  factory: () => connectionFactory(endpoint)
})

export const UserActionConnection = new InjectionToken<HubConnection>('UserActionConnection', connectionProvider('signalr/user-action'));
export const EditorConnection = new InjectionToken<HubConnection>('EditorConnection', connectionProvider('signalr/editor'));
