import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateSrlComponent } from './create-srl.component';
import { CreateSrlRoutingModule } from './create-srl.routing';
import { CreateSrlContactComponent } from './create-srl-contact/create-srl-contact.component';
import { ReactiveFormsModule } from '@angular/forms';
import { CreateSrlAssociatesComponent } from './create-srl-associates/create-srl-associates.component';
import { AssociateDataComponent } from './associate-data/associate-data.component';
import { PrimengModule } from 'src/app/shared/modules/primeng.module';
@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    CreateSrlRoutingModule,
    PrimengModule,
  ],
  providers: [
  ],
  declarations: [
    CreateSrlComponent,
    CreateSrlContactComponent,
    CreateSrlAssociatesComponent,
    AssociateDataComponent
  ]
})
export class CreateSrlModule { }
