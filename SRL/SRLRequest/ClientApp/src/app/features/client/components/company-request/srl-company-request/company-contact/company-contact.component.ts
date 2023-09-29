import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CompanyRequestService } from '../../../../../shared/models/company-request.service';
import { Contact } from '../../../../../shared/models/contact.model';
import { CompanyRequest } from '../../../../../shared/models/company-request.model';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-company-contact',
  templateUrl: './company-contact.component.html',
  styleUrls: ['./company-contact.component.scss']
})
export class CompanyContactComponent implements OnInit, OnDestroy {

  public contactFormGroup!: FormGroup;
  constructor(fb: FormBuilder,
    private _companyRequestService: CompanyRequestService,
    private _router: Router,
    private _route: ActivatedRoute) {
    this.contactFormGroup = fb.group({
      'phoneNumber': [''],
      'email': ['']
    });
    _companyRequestService.companyRequest$.subscribe(r => {
      const c = r?.contact;
      this.contactFormGroup.setValue({
          'phoneNumber': c?.phoneNumber??'',
          'email': c?.email??''
        });
    });
   }
  ngOnDestroy() {
    //this.save();
  }

  ngOnInit() {
  }

  async save() {
    const c: Contact = this.contactFormGroup.value;
    console.error(c);

    await this._companyRequestService.updateContact(c);
  }

  public async nextPage() {
    await this.save();
    this._companyRequestService.gotoCompanyAssociates(this._router, this._route);
  }
}
