import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SRLCompanyRequestComponent } from './srl-company-request.component';
import { SRLCompanyRequestNamesComponent } from './srl-company-request-names/srl-company-request-names.component';
import { CompanyLocationsComponent } from './company-locations/company-locations.component';
import { CompanyContactComponent } from './company-contact/company-contact.component';
import { CompanyActivitiesComponent } from './company-activities/company-activities.component';
import { CompanyAssociatesComponent } from './company-associates/company-associates.component';


const routes: Routes = [
  {
    path: '', component: SRLCompanyRequestComponent,
      children: [
        { path: '', redirectTo: 'contact', pathMatch: 'full' },
        { path: 'contact', component: CompanyContactComponent },
        { path: 'associates', component: CompanyAssociatesComponent },
        { path: 'location', component: CompanyLocationsComponent },
        { path: 'activities', component: CompanyActivitiesComponent },
        { path: 'names', component: SRLCompanyRequestNamesComponent }
      ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SRLCompanyRequestRoutingModule { }
