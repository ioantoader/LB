import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-create-srl-contact',
  templateUrl: './create-srl-contact.component.html',
  styleUrls: ['./create-srl-contact.component.scss']
})
export class CreateSrlContactComponent implements OnInit {

  public contactFormGroup!: FormGroup;
  constructor(fb: FormBuilder) {
    this.contactFormGroup = fb.group({
      'phoneNumber': [],
      'email': []
    });
   }

  ngOnInit() {
  }

}
