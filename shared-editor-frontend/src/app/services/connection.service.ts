import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class ConnectionService {
  readonly connection = this.getConnectionTo('http://localhost:5000/connection-state');

  constructor(private hubConnectionBuilder: HubConnectionBuilder) { }
  
  connect(): Promise<void> {
    return this.connection.start();
  }

  disconnect(): Promise<void> {
    return this.connection.stop();
  }

  private getConnectionTo(url: string): HubConnection {
    return this.hubConnectionBuilder.withUrl(url).build();
  }
}
