import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClientLayoutComponent } from './client.layout.component';
import { ClientRoutes } from './client.routing';
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [
    CommonModule,
    ClientRoutes,
  ],
  exports: [
    RouterModule,
  ],
  declarations: [ClientLayoutComponent]
})
export class ClientModule { }
