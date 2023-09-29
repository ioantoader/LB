import { Component, OnInit } from '@angular/core';
import { CompanyRequestService } from '../../../../../shared/models/company-request.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-company-activities',
  templateUrl: './company-activities.component.html',
  styleUrls: ['./company-activities.component.scss']
})
export class CompanyActivitiesComponent implements OnInit {

  constructor(private _companyRequestService: CompanyRequestService,
    private _router: Router, private _route: ActivatedRoute) { }

  ngOnInit() {
  }

  public prevPage() {
    this._companyRequestService.gotoCompanyLocations(this._router, this._route);
  }

  public nextPage() {
    this._companyRequestService.gotoCompanyNames(this._router, this._route);
  }

}
