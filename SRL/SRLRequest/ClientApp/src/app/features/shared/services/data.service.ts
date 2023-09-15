import { Injectable, Optional,SkipSelf } from '@angular/core';
import { PersonData } from '../models/person-data.model';
import { HttpClient } from '@angular/common/http';
import { CompanyRequest } from '../models/company-request.model';
import { firstValueFrom } from 'rxjs';
import { Contact } from '../models/contact.model';
@Injectable()
export class DataService {

  constructor(private _httpClient: HttpClient) {
  }
  public addAssociate(companyId: string ,associate: PersonData): Promise<PersonData> {
    associate.id = crypto.randomUUID();
    return Promise.resolve(associate);
  }

  public updateAssociate(associate: PersonData): Promise<void> {
    return Promise.resolve();
  }

  public deleteAssociate(associateId: string): Promise<void> {

    return Promise.resolve();
  }

  /*
  public updateCompanyRequest(companyRequest: CompanyRequest): Promise<CompanyRequest> {
    console.error(companyRequest);
    const uri = `/api/CompanyRequest`;
    return firstValueFrom(this._httpClient.patch<CompanyRequest>(uri, companyRequest));
  }
  */

  public updateContact(requestId: string, contact: Contact): Promise<CompanyRequest> {
    const uri = `/api/CompanyRequest/${requestId}/contact`;

    return firstValueFrom(this._httpClient.put<CompanyRequest>(uri, contact));
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

