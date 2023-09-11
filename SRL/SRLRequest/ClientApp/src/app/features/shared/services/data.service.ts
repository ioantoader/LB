import { Injectable, Optional,SkipSelf } from '@angular/core';
import { PersonData } from '../models/person-data.model';
@Injectable()
export class DataService {

  constructor() { }
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

}


export function DATA_PROVIDER_FACTORY(
  parentAuthService: DataService) {
  return parentAuthService || new DataService();
}

export const DATA_PROVIDER = {
  provide: DataService,
  deps: [[new Optional(), new SkipSelf(), DataService]],
  useFactory: DATA_PROVIDER_FACTORY
};

