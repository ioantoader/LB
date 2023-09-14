import { Injectable } from "@angular/core";
import { CompanyRequest } from "./company-request.model";

@Injectable()
export class CompanyRequestService {
  private _companyRequest!: CompanyRequest
  constructor() {

  }

  public get companyRequest(): CompanyRequest {
    return this._companyRequest;
  }
  public set companyRequest(value: CompanyRequest) {
    this._companyRequest = value;
  }
}
