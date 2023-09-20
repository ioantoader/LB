import { Injectable } from "@angular/core";
import { CompanyRequest } from "./company-request.model";
import { DataService } from "../services/data.service";
import { Contact } from "./contact.model";
import { MenuItem } from "primeng/api";
import { ActivatedRoute, Router } from "@angular/router";
import { PersonData } from "./person-data.model";

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
    const requestId = this._companyRequest?.id ?? crypto.randomUUID();
    this.companyRequest = await this._dataService.updateContact(requestId, value);
  }
  public async addAssociate(associateData: PersonData) {
    const requestId = this._companyRequest?.id ?? crypto.randomUUID();
    this.companyRequest = await this._dataService.addAssociate(requestId, associateData);
  }

  public async updateAssociate(associateData: PersonData) {    
    this.companyRequest = await this._dataService.updateAssociate(associateData);
  }

  public async deleteAssociate(associateId: string) {
    await this._dataService.deleteAssociate(associateId);
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
