import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { PersonData } from 'src/app/features/shared/models/person-data.model';

@Component({
  selector: 'app-location-owner',
  templateUrl: './location-owner.component.html',
  styleUrls: ['./location-owner.component.scss']
})
export class LocationOwnerComponent implements OnInit {

  private _locationOwner?: Partial<PersonData>;
  public locationOwnerFormGroup!: FormGroup;
  constructor(fb: FormBuilder, private _dialogRef: DynamicDialogRef, private _dialogConfig: DynamicDialogConfig) {
    this._locationOwner = _dialogConfig.data;
    this.locationOwnerFormGroup = fb.group({
      'id': [this._locationOwner?.id],
      'contact': fb.group(
        {
          'lastName': [this._locationOwner?.contact?.lastName],
          'firstName': [this._locationOwner?.contact?.firstName]
        }
      ),
      'identityDocument': fb.group(
        {
          'serial': this._locationOwner?.identityDocument?.serial,
          'number': this._locationOwner?.identityDocument?.number,
          'cnp': this._locationOwner?.identityDocument?.cnp,
        })

    })
  }

  ngOnInit() {
  }

  public save() {
    const t: Partial<PersonData> = this.locationOwnerFormGroup.value;
    t.id = this._locationOwner?.id;
    this._locationOwner = t;
    this._dialogRef.close(this._locationOwner);
  }

}
