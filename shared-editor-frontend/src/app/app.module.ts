import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EditorTextPipe } from './features/editor';
import { UserActionPipe } from './features/user-action';

@NgModule({
  declarations: [
    AppComponent,
    UserActionPipe,
    EditorTextPipe
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
