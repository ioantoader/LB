import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CompanyRequestService } from '../../../../../shared/models/company-request.service';
import { Contact } from '../../../../../shared/models/contact.model';
import { CompanyRequest } from '../../../../../shared/models/company-request.model';

@Component({
  selector: 'app-company-contact',
  templateUrl: './company-contact.component.html',
  styleUrls: ['./company-contact.component.scss']
})
export class CompanyContactComponent implements OnInit, OnDestroy {

  public contactFormGroup!: FormGroup;
  constructor(fb: FormBuilder, private _companyRequestService: CompanyRequestService) {
    const c = _companyRequestService.companyRequest?.contact;
    this.contactFormGroup = fb.group({
      'phoneNumber': [c?.phoneNumber],
      'email': [c?.email]
    });
   }
  ngOnDestroy() {
    this.save();
    }

  ngOnInit() {
  }

  async save() {
    const c: Contact = this.contactFormGroup.value;
    console.error(c);

    await this._companyRequestService.updateContact(c);
  }

}
