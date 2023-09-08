import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CreateSrlComponent } from './create-srl.component';
import { CreateSrlContactComponent } from './create-srl-contact/create-srl-contact.component';
import { CreateSrlAssociatesComponent } from './create-srl-associates/create-srl-associates.component';
import { CreateSrlLocationsComponent } from './create-srl-locations/create-srl-locations.component';
import { CreateSrlActivitiesComponent } from './create-srl-activities/create-srl-activities.component';
import { CreateSrlNamesComponent } from './create-srl-names/create-srl-names.component';


const routes: Routes = [
  {
    path: '',component: CreateSrlComponent,
      children: [
        {path: '', redirectTo: 'contact', pathMatch: 'full'},
        {path: 'contact', component: CreateSrlContactComponent},
        {path: 'associates', component: CreateSrlAssociatesComponent},
        {path: 'location', component: CreateSrlLocationsComponent},
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
