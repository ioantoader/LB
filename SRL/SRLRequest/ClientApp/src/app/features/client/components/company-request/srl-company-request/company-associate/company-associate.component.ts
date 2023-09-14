import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { PersonData } from 'src/app/features/shared/models/person-data.model';

@Component({
  selector: 'app-company-associate',
  templateUrl: './company-associate.component.html',
  styleUrls: ['./company-associate.component.scss']
})
export class CompanyAssociateComponent implements OnInit {
  private _associateData?: PersonData
  public  associateDataFormGroup: FormGroup;
  constructor(private _dialogRef: DynamicDialogRef, private _config: DynamicDialogConfig,  fb: FormBuilder) {
    this._associateData = _config.data;
    this.associateDataFormGroup = fb.group(
      {
        'id': [this._associateData?.id],
        'contact':  fb.group({
          'firstName':    [this._associateData?.contact?.firstName],
          'lastName':     [this._associateData?.contact?.lastName],
          'phoneNumber':  [this._associateData?.contact?.phoneNumber],

        }),
        'identityDocument': fb.group(
          {
            'serial':         [this._associateData?.identityDocument?.serial],
            'number':         [this._associateData?.identityDocument.number],
            'cnp':            [this._associateData?.identityDocument?.cnp],
            'birthCountry':   [this._associateData?.identityDocument?.birthCountry],
            'birthState':     [this._associateData?.identityDocument?.birthState],
            'birthCity':      [this._associateData?.identityDocument?.birthCity],
            'citizenship':    [this._associateData?.identityDocument?.citizenship],
            'issueDate':      [this._associateData?.identityDocument?.issueDate],
            'expirationDate': [this._associateData?.identityDocument?.expirationDate],
            'issueBy':        [this._associateData?.identityDocument?.issueBy],
            'birthDate':      [this._associateData?.identityDocument?.birthDate],
          }),
          'address': fb.group(
            {
              'country':    [this._associateData?.address?.country],
              'state':      [this._associateData?.address?.state],
              'city':       [this._associateData?.address?.city],
              'street':     [this._associateData?.address?.street],
              'number':     [this._associateData?.address?.number],
              'postalCode': [this._associateData?.address?.postalCode],
              'block':      [this._associateData?.address?.block],
              'stair':      [this._associateData?.address?.stair],
              'floor':      [this._associateData?.address?.floor],
              'apartment':  [this._associateData?.address?.apartment],
            }
          )
      }
    )
  }

  ngOnInit() {
  }

  public save(): void {
    const id = this._associateData?.id;
    const a:PersonData = this.associateDataFormGroup.value;
    a.id = id;
    this._associateData = a;
    this._dialogRef.close(this._associateData);
  }
}
