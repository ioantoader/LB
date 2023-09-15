import { Injectable } from "@angular/core";
import { CompanyRequest } from "./company-request.model";
import { DataService } from "../services/data.service";
import { Contact } from "./contact.model";

@Injectable()
export class CompanyRequestService {
  private _companyRequest!: CompanyRequest
  constructor(private _dataService: DataService) {

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
}
