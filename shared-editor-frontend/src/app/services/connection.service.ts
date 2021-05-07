import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ConnectionService {
  private connectionSubject = new BehaviorSubject<HubConnection | null>(null);
  public readonly connection = this.connectionSubject.asObservable();

  constructor(private hubConnectionBuilder: HubConnectionBuilder) { }

  connectTo(url: string) {
    const connection = this.hubConnectionBuilder.withUrl('/connection-state').build();
    this.connectionSubject.next(connection);
  }
}
