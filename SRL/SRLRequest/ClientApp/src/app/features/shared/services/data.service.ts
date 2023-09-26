import { Injectable, Optional,SkipSelf } from '@angular/core';
import { PersonData } from '../models/person-data.model';
import { HttpClient } from '@angular/common/http';
import { CompanyRequest } from '../models/company-request.model';
import { firstValueFrom } from 'rxjs';
import { Contact } from '../models/contact.model';
import { CompanyLocation } from '../models/company-location.model';
@Injectable()
export class DataService {
  private baseUrl: string = '';
  constructor(private _httpClient: HttpClient) {
  }
  public addAssociate(companyId: string, associate: PersonData): Promise<PersonData> {
    const uri = `${this.baseUrl}/api/CompanyRegistration/requests/${companyId}/associates`;
    return firstValueFrom(this._httpClient.post<PersonData>(uri, associate));
  }

  public updateAssociate(associate: PersonData): Promise<PersonData> {
    const uri = `${this.baseUrl}/api/CompanyRegistration/requests/associates`;
    return firstValueFrom(this._httpClient.put<PersonData>(uri, associate));

  }

  public async deleteAssociate(associateId: string): Promise<void> {
    const uri = `${this.baseUrl}/api/CompanyRegistration/requests/associates/${associateId}`;

    await firstValueFrom(this._httpClient.delete(uri));
  }

  public addLocation(companyId: string, location: CompanyLocation): Promise<CompanyLocation> {
    const uri = `${this.baseUrl}/api/CompanyRegistration/requests/${companyId}/locations`;
    return firstValueFrom(this._httpClient.post<CompanyLocation>(uri, location));
  }
  /*
  public updateCompanyRequest(companyRequest: CompanyRequest): Promise<CompanyRequest> {
    console.error(companyRequest);
    const uri = `/api/CompanyRequest`;
    return firstValueFrom(this._httpClient.patch<CompanyRequest>(uri, companyRequest));
  }
  */

  public updateContact(requestId: string, contact: Contact): Promise<CompanyRequest> {
    const uri = `${this.baseUrl}/api/CompanyRegistration/requests/${requestId}/contact`;

    return firstValueFrom(this._httpClient.put<CompanyRequest>(uri, contact));
  }

  public createCompanyRegistrationRequest(): Promise<CompanyRequest> {
    const uri = `${this.baseUrl}/api/CompanyRegistration/requests`;
    return firstValueFrom(this._httpClient.post<CompanyRequest>(uri, null));
  }
  
}


export function DATA_PROVIDER_FACTORY(
  parentAuthService: DataService, httpClient: HttpClient) {
  return parentAuthService || new DataService(httpClient);
}

export const DATA_PROVIDER = {
  provide: DataService,
  deps: [[new Optional(), new SkipSelf(), DataService], HttpClient],
  useFactory: DATA_PROVIDER_FACTORY
};

