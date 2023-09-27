import { Injectable } from "@angular/core";
import { CompanyRequest } from "./company-request.model";
import { DataService } from "../services/data.service";
import { Contact } from "./contact.model";
import { MenuItem } from "primeng/api";
import { ActivatedRoute, Router } from "@angular/router";
import { PersonData } from "./person-data.model";
import { CompanyLocation } from "./company-location.model";
import { BehaviorSubject, Observable, firstValueFrom } from "rxjs";

@Injectable()
export class CompanyRequestService {
  private _companyRequest: BehaviorSubject<CompanyRequest> = new BehaviorSubject<CompanyRequest>(undefined!)
  public companyRequest$: Observable<CompanyRequest> = this._companyRequest.asObservable();
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

  public async loadCompanyRequest(requestId: string): Promise<CompanyRequest> {
    const r = await this._dataService.getCompanyRegistrationRequest(requestId);
    this.setCompanyRequest(r);
    return r;

  }
  public setCompanyRequest(value: CompanyRequest) {
    this._companyRequest.next(value);
  }

  private async currentCompanyRequest(): Promise<CompanyRequest> {
    return await firstValueFrom(this.companyRequest$);
  }
  public async CompanyRequest(requestId: string): Promise<CompanyRequest> {
    const r = await this._dataService.getCompanyRegistrationRequest(requestId);
    this.setCompanyRequest(r);
    return r;
  }

  public async updateContact(value: Contact) {
    const requestId = (await this.currentCompanyRequest())?.id!;
    this.setCompanyRequest(await this._dataService.updateContact(requestId, value));
  }
  public async addAssociate(associate: PersonData): Promise<PersonData> {
    const requestId = (await this.currentCompanyRequest())?.id!;
    associate = await this._dataService.addAssociate(requestId, associate);

    const companyRequest = await this.currentCompanyRequest();
    let associates = companyRequest.associates;
    if (!associates) {
      associates = companyRequest.associates = [];
    }
    associates.push(associate);
    return associate;
  }

  public async updateAssociate(associateData: PersonData): Promise<PersonData> {    
    const response = await this._dataService.updateAssociate(associateData);
    const companyRequest = await this.currentCompanyRequest();
    const asociates = companyRequest.associates;
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
    const companyRequest = await this.currentCompanyRequest();
    const asociates = companyRequest.associates;
    if (asociates) {
      companyRequest.associates = asociates.filter(a => a.id?.toUpperCase() !== associateId.toUpperCase());
    }

  }

  public async addLocation(location: CompanyLocation): Promise<CompanyLocation> {
    const companyRequest = await this.currentCompanyRequest();
    const requestId = companyRequest?.id!
    location = await this._dataService.addLocation(requestId, location);
    let locations = companyRequest.locations;
    if (!locations) {
      locations = companyRequest.locations = [];
    }
    locations.push(location);
    return location;
  }

  public async updateLocation(location: CompanyLocation): Promise<CompanyLocation> {
    const companyRequest = await this.currentCompanyRequest();
    location = await this._dataService.updateLocation(location);
    let locations = companyRequest.locations;
    if (!locations) {
      locations = companyRequest.locations = [];
    }
    const idx = locations.findIndex(l => l.id?.toUpperCase() === location.id)
    if (idx >= 0) {
      locations[idx] = location;
    }

    return location;
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
