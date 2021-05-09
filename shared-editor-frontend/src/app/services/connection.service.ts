import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ConnectionService {
  readonly connection = this.getConnectionTo('connection-state');

  constructor(private hubConnectionBuilder: HubConnectionBuilder) { }
  
  connect(): Promise<void> {
    return this.connection.start();
  }

  disconnect(): Promise<void> {
    return this.connection.stop();
  }

  private getConnectionTo(url: string): HubConnection {
    const urlWithPrefix = environment.endpointPrefixes.signalr + url;
    return this.hubConnectionBuilder.withUrl(urlWithPrefix).build();
  }
}
