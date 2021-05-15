import { Component, OnInit } from '@angular/core';
import { HubConnectionBuilder } from '@microsoft/signalr';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  connections = {
    userAction: this.hubConnectionBuilder.withUrl('signalr/user-action').build(),
    editor: this.hubConnectionBuilder.withUrl('signalr/editor').build()
  };

  constructor(private hubConnectionBuilder: HubConnectionBuilder) { }

  ngOnInit(): void {
    this.connections.userAction.on('user-action', () => console.log('user-action'));
    // this.connections.editor.on('editor', () => console.log('editor'));

    this.connections.userAction.start().then(() => {
      Array.from({ length: 10 }).forEach(() => this.connections.userAction.send('OnModelChange', 0))
      console.log('connection started')
    });
    // this.connections.editor.start().then(() => Array.from({ length: 10 }).forEach(() => this.connections.editor.send('OnModelChange', { text: 'ooooooo' })));
  }
}
