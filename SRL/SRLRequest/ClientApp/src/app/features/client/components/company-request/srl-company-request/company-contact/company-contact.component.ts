import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CompanyRequestService } from '../../../../../shared/models/company-request.service';

@Component({
  selector: 'app-company-contact',
  templateUrl: './company-contact.component.html',
  styleUrls: ['./company-contact.component.scss']
})
export class CompanyContactComponent implements OnInit {

  public contactFormGroup!: FormGroup;
  constructor(fb: FormBuilder, companyRequestService: CompanyRequestService) {
    this.contactFormGroup = fb.group({
      'phoneNumber': [],
      'email': []
    });
   }

  ngOnInit() {
  }

}
