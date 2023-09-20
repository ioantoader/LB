import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DataService } from '../../../shared/services/data.service';

@Component({
  selector: 'app-company-request',
  templateUrl: './company-request.component.html',
  styleUrls: ['./company-request.component.css']
})
export class CompanyRequestComponent implements OnInit {

  constructor(private _dataService: DataService,
    private _route: ActivatedRoute, private _router: Router) {
  }

  ngOnInit() {
  }

  public async createSRLRegistrationRequest(): Promise<void> {
    const request = await this._dataService.createCompanyRegistrationRequest()
    const s: { [k: string]: any } = {};
    s[request.id!] = request;
    this._router.navigate(['requests', 'srl', request.id],
      {
        relativeTo: this._route.parent,
        state: s
      });
  }
}
