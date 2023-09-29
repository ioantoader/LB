import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { PrimengModule } from 'src/app/shared/modules/primeng.module';
import { LocationComponent } from './location/location.component';
import { LocationOwnerComponent } from './location/location-owner/location-owner.component';
import { SRLCompanyRequestComponent } from './srl-company-request.component';
import { SRLCompanyRequestRoutingModule } from './srl-company-request.routing';
import { CompanyLocationsComponent } from './company-locations/company-locations.component';
import { CompanyContactComponent } from './company-contact/company-contact.component';
import { CompanyAssociateComponent } from './company-associate/company-associate.component';
import { CompanyAssociatesComponent } from './company-associates/company-associates.component';
import { CompanyActivitiesComponent } from './company-activities/company-activities.component';
import { SRLCompanyRequestNamesComponent } from './srl-company-request-names/srl-company-request-names.component';
@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    SRLCompanyRequestRoutingModule,
    PrimengModule,
  ],
  providers: [
  ],
  declarations: [
    SRLCompanyRequestComponent,
    CompanyContactComponent,
    CompanyAssociatesComponent,
    CompanyAssociateComponent,
    CompanyLocationsComponent,
    LocationComponent,
    LocationOwnerComponent,
    CompanyActivitiesComponent,
    SRLCompanyRequestNamesComponent
  ]
})
export class SRLCompanyRequestModule { }
