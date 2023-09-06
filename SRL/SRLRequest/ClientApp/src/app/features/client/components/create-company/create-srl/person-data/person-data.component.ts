import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { PersonData } from 'src/app/features/shared/models/person-data.model';

@Component({
  selector: 'app-person-data',
  templateUrl: './person-data.component.html',
  styleUrls: ['./person-data.component.scss']
})
export class PersonDataComponent implements OnInit {
  private _personData?: PersonData
  public  personDataFormGroup: FormGroup;
  constructor(private _dialogRef: DynamicDialogRef, private _config: DynamicDialogConfig,  fb: FormBuilder) {
    this._personData = _config.data;
    this.personDataFormGroup = fb.group(
      {
        'firstName': [this._personData?.firstName],
        'lastName': [this._personData?.lastName],
        'phone': [this._personData?.phone],
        'identityDocument': fb.group(
          {
            'serial': [this._personData?.identityDocument?.serial],
            'number': [this._personData?.identityDocument.number],
            'cnp': [this._personData?.identityDocument?.cnp],
            'birthCountry': [this._personData?.identityDocument?.birthCountry],
            'birthState': [this._personData?.identityDocument?.birthState],
            'birthCity': [this._personData?.identityDocument?.birthCity],
            'citizenship': [this._personData?.identityDocument?.citizenship],
            'issueDate': [this._personData?.identityDocument?.issueDate],
            'expirationDate': [this._personData?.identityDocument?.expirationDate],
            'issueBy': [this._personData?.identityDocument?.issueBy],
            'birthDate': [this._personData?.identityDocument?.birthDate],
          }),
          'address': fb.group(
            {
              'country': [this._personData?.address?.country],
              'state': [this._personData?.address?.state],
              'city': [this._personData?.address?.city],
              'street': [this._personData?.address?.street],
              'number': [this._personData?.address?.number],
              'postalCode': [this._personData?.address?.postalCode],
              'block': [this._personData?.address?.block],
              'stair': [this._personData?.address?.stair],
              'floor': [this._personData?.address?.floor],
              'apartment': [this._personData?.address?.apartment],
            }
          )
      }
    )
  }

  ngOnInit() {
  }

  public save(): void {
    this._personData = this.personDataFormGroup.value;
    this._dialogRef.close(this._personData);
  }
}
