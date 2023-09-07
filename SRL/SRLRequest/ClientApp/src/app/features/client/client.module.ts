import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClientLayoutComponent } from './client.layout.component';
import { ClientRoutes } from './client.routing';
import { RouterModule } from '@angular/router';
import { AppLayoutModule } from './../../layout/app.layout.module';
import { CLIENT_MENU_PROVIDER } from './service/client.menu.service';
import { CreateCompanyComponent } from './components/create-company/create-company.component';
import { DATA_PROVIDER } from '../shared/services/data.service';
import { PrimengModule } from 'src/app/shared/modules/primeng.module';

@NgModule({
  imports: [
    CommonModule,
    ClientRoutes,
    PrimengModule,
    AppLayoutModule
  ],
  exports: [
    RouterModule
  ],
  providers: [
    CLIENT_MENU_PROVIDER,
    DATA_PROVIDER
  ],
  declarations: [ClientLayoutComponent, CreateCompanyComponent]
})
export class ClientModule { }
