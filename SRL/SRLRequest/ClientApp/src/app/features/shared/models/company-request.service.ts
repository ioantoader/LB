import { Injectable } from "@angular/core";
import { CompanyRequest } from "./company-request.model";
import { DataService } from "../services/data.service";
import { Contact } from "./contact.model";
import { MenuItem } from "primeng/api";
import { ActivatedRoute, Router } from "@angular/router";
import { PersonData } from "./person-data.model";
import { CompanyLocation } from "./company-location.model";


@Injectable()
export class CompanyRequestService {
  private _companyRequest!: CompanyRequest

  public readonly steps: MenuItem[] = [];
  constructor(private _dataService: DataService) {
    this.steps = [
      {
        label: 'Date de contact',
        routerLink: 'contact'
      },
      {
        label: 'Persoanele din firma',
        routerLink: 'associates'
      },
      {
        label: 'Sediul Firmei',
        routerLink: 'location'
      },
      {
        label: 'Activitati autorizate',
        routerLink: 'activities'
      },
      {
        label: 'Denumirea Firmei',
        routerLink: 'names'
      }
    ];

  }

  public get companyRequest(): CompanyRequest {
    return this._companyRequest;
  }
  public set companyRequest(value: CompanyRequest) {
    this._companyRequest = value;
  }

  public async updateContact(value: Contact) {
    const requestId = this._companyRequest?.id!
    this.companyRequest = await this._dataService.updateContact(requestId, value);
  }
  public async addAssociate(associateData: PersonData): Promise<PersonData> {
    const requestId = this._companyRequest?.id!
    const p = await this._dataService.addAssociate(requestId, associateData);
    let associates = this.companyRequest.associates;
    if (!associates) {
      associates = this.companyRequest.associates = [];
    }
    associates.push(p);
    return p;
  }

  public async updateAssociate(associateData: PersonData): Promise<PersonData> {    
    const response = await this._dataService.updateAssociate(associateData);
    const asociates = this.companyRequest.associates;
    if (asociates) {
      const idx = asociates.findIndex(p => p.id?.toUpperCase() == response.id?.toUpperCase());
      if (idx >= 0) {
        asociates[idx] = response;
      }
    }
    return response;
  }

  public async deleteAssociate(associateId: string) {
    await this._dataService.deleteAssociate(associateId);
    const asociates = this.companyRequest.associates;
    if (asociates) {
      this.companyRequest.associates = asociates.filter(a => a.id?.toUpperCase() !== associateId.toUpperCase());
    }

  }

  public async addLocation(location: CompanyLocation): Promise<CompanyLocation> {
    const requestId = this._companyRequest?.id!
    const p = await this._dataService.addLocation(requestId, location);
    let locations = this.companyRequest.locations;
    if (!locations) {
      locations = this.companyRequest.locations = [];
    }
    locations.push(p);
    return p;
  }

  public gotoCompanyAssociates(router: Router, route: ActivatedRoute) {
    router.navigate([this.steps[1].routerLink], { relativeTo: route.parent });
  }

  public gotoCompanyContact(router: Router, route: ActivatedRoute) {
    router.navigate([this.steps[0].routerLink], { relativeTo: route.parent });
  }

  public gotoCompanyLocations(router: Router, route: ActivatedRoute) {
    router.navigate([this.steps[2].routerLink], { relativeTo: route.parent });
  }

}
