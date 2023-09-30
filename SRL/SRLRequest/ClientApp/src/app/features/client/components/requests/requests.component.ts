import { Component, OnInit } from '@angular/core';
import { CompanyRequest } from '../../../shared/models/company-request.model';
import { DataService } from '../../../shared/services/data.service';

@Component({
  selector: 'app-requests',
  templateUrl: './requests.component.html',
  styleUrls: ['./requests.component.scss']
})
export class RequestsComponent implements OnInit {
  public requests: CompanyRequestViewModel[] = [];
  constructor(private _dataService: DataService) { }

  ngOnInit() {
    this.loadData();
  }

  private async loadData() {
    const rs = await this._dataService.getCompanyRegistrationRequests()
    this.requests = rs.map(r => new CompanyRequestViewModel(r));
  }

}

class CompanyRequestViewModel {
  constructor(private _companyRequest: CompanyRequest) {

  }

  public get id(): string | undefined {
    return this._companyRequest?.id;
  }

  public get contactEmail(): string | undefined {
    return this._companyRequest?.contact?.email;
  }
  public get contactPhone(): string | undefined {
    return this._companyRequest?.contact?.phoneNumber;
  }

}
