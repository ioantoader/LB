import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CreateSrlComponent } from './create-srl.component';
import { CreateSrlContactComponent } from './create-srl-contact/create-srl-contact.component';
import { CreateSrlPersonsComponent } from './create-srl-persons/create-srl-persons.component';
import { CreateSrlLocationComponent } from './create-srl-location/create-srl-location.component';
import { CreateSrlActivitiesComponent } from './create-srl-activities/create-srl-activities.component';
import { CreateSrlNamesComponent } from './create-srl-names/create-srl-names.component';


const routes: Routes = [
  {
    path: '',component: CreateSrlComponent,
      children: [
        {path: '', redirectTo: 'contact', pathMatch: 'full'},
        {path: 'contact', component: CreateSrlContactComponent},
        {path: 'persons', component: CreateSrlPersonsComponent},
        {path: 'location', component: CreateSrlLocationComponent},
        {path: 'activities', component: CreateSrlActivitiesComponent},
        {path: 'names', component: CreateSrlNamesComponent}
      ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CreateSrlRoutingModule { }
