import { Component, Inject } from '@angular/core';
import { HubConnection } from '@microsoft/signalr';
import { Observable, pipe } from 'rxjs';
import { map } from 'rxjs/operators';
import { Editor } from './features/editor';
import { UserAction } from './features/user-action';
import { UserActionConnection, EditorConnection } from './shared/connections';

const mapToText = pipe(map((editor: Editor) => editor.text));

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  lastUserAction: Observable<UserAction> = this.fromConnectionToObservable<UserAction>(this.userActionConnection, 'user-action');
  editorText: Observable<string> = this.fromConnectionToObservable<Editor>(this.editorConnection, 'editor').pipe(mapToText);

  constructor(
    @Inject(UserActionConnection) private userActionConnection: HubConnection,
    @Inject(EditorConnection) private editorConnection: HubConnection
  ) { }

  private fromConnectionToObservable<T>(connection: HubConnection, method: string): Observable<T> {
    return new Observable(observer => {
      connection.on(method, param => observer.next(param));
      connection.start();
    });
  }
}
