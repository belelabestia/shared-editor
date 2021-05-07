import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HubConnectionBuilder } from '@microsoft/signalr';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [
    {
      provide: HubConnectionBuilder,
      useClass: HubConnectionBuilder
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
