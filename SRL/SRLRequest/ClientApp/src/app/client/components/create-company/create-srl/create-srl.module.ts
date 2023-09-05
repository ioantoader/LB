import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateSrlComponent } from './create-srl.component';
import { CreateSrlRoutingModule } from './create-srl.routing';
import { StepsModule } from 'primeng/steps';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { TabViewModule } from 'primeng/tabview';
import { DropdownModule } from 'primeng/dropdown';
import { CalendarModule } from 'primeng/calendar';
import { CreateSrlContactComponent } from './create-srl-contact/create-srl-contact.component';
import { CreateSrlPersonsComponent } from './create-srl-persons/create-srl-persons.component';
import { PersonDataComponent } from './person-data/person-data.component';
@NgModule({
  imports: [
    CommonModule,
    CreateSrlRoutingModule,
    StepsModule,
    InputTextModule,
    ButtonModule,
    DynamicDialogModule,
    TabViewModule,
    DropdownModule,
    CalendarModule
  ],
  providers: [
  ],
  declarations: [
    CreateSrlComponent,
    CreateSrlContactComponent,
    CreateSrlPersonsComponent,
    PersonDataComponent
  ]
})
export class CreateSrlModule { }
