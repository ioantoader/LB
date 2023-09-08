import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CompanyLocation } from 'src/app/features/shared/models/company-location.model';

@Component({
  selector: 'app-location',
  templateUrl: './location.component.html',
  styleUrls: ['./location.component.scss']
})
export class LocationComponent implements OnInit {
  private _location?: CompanyLocation;
  public locationFormGroup!: FormGroup;
  constructor(fb: FormBuilder) {
    this.locationFormGroup = fb.group({
      'address': fb.group(
        {
          'country': this._location?.address?.country,
          'state': this._location?.address?.state,
          'city': this._location?.address?.city,
          'street': this._location?.address?.street,
          'number': this._location?.address?.number,
          'postalCode': this._location?.address?.postalCode,
          'block': this._location?.address?.block,
          'stair': this._location?.address?.stair,
          'floor': this._location?.address?.floor,
          'apartment': this._location?.address?.apartment,
        }
      )
    })
  }

  ngOnInit() {
  }



}
