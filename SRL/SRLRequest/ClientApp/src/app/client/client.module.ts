import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClientLayoutComponent } from './client.layout.component';
import { ClientRoutes } from './client.routing';
import { RouterModule } from '@angular/router';
import { AppLayoutModule } from '../layout/app.layout.module';
import { CLIENT_MENU_PROVIDER } from './service/client.menu.service';

@NgModule({
  imports: [
    CommonModule,
    ClientRoutes,
    AppLayoutModule,
  ],
  exports: [
    RouterModule
  ],
  providers: [
    CLIENT_MENU_PROVIDER
  ],
  declarations: [ClientLayoutComponent]
})
export class ClientModule { }
