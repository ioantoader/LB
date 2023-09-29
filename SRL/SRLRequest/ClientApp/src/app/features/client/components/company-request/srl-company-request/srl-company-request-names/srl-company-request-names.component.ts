import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CompanyRequestService } from '../../../../../shared/models/company-request.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CompanyNames } from '../../../../../shared/models/companyNames.model';

@Component({
  selector: 'app-srl-company-request-names',
  templateUrl: './srl-company-request-names.component.html',
  styleUrls: ['./srl-company-request-names.component.scss']
})
export class SRLCompanyRequestNamesComponent implements OnInit {

  public namesFormGroup: FormGroup
  constructor(_fb: FormBuilder,
    private _companyRequestService: CompanyRequestService,
    private _router: Router,
    private _route: ActivatedRoute) {
    this.namesFormGroup = _fb.group({
      'name1': [''],
      'name2': [''],
      'name3': [''],
    });

    this._companyRequestService.companyRequest$.subscribe(r => {
        var names = r?.names;
        this.namesFormGroup.setValue({
          'name1': names?.name1??'',
          'name2': names?.name2??'',
          'name3': names?.name3??'',
        });
    });
  }

  ngOnInit() {
  }

  public previewPage() {
    this._companyRequestService.gotoCompanyActivities(this._router, this._route);
  }
  public nextPage() {
    this.save();
  }
  public save() {
    const cn = <CompanyNames>this.namesFormGroup.value;
    this._companyRequestService.updateNames(cn);

  }
}
