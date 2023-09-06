import { Component, OnDestroy, OnInit } from '@angular/core';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { PersonDataComponent } from '../person-data/person-data.component';
import { PersonData } from 'src/app/features/shared/models/person-data.model';
import { IdentityDocument } from 'src/app/features/shared/models/Identity-document.model';

@Component({
  selector: 'app-create-srl-persons',
  templateUrl: './create-srl-persons.component.html',
  styleUrls: ['./create-srl-persons.component.scss'],
  providers: [DialogService]
})
export class CreateSrlPersonsComponent implements OnInit, OnDestroy {

  private dialogRef?: DynamicDialogRef;
  public associates: PersonDataGridViewModel[] = [];
  constructor(public dialogService: DialogService) { }

  ngOnInit() {
  }

  public addPerson() {
    let id: Partial<IdentityDocument> = {
      serial: 'xb',
      number: '12345',
      cnp: '1770477',
      birthCountry: 'Romania',
      birthState: 'Bistrita-Nasaud',
      birthCity:'Rebra',
      citizenship: 'Romana',
      issueDate: new Date(2020,2,2),
      expirationDate: new Date(2030,2,2),
      issueBy: 'Politie',
      birthDate: new Date(1977,3,13),
    }
    let p: Partial<PersonData> = {
      firstName: 'Ioan',
      lastName: 'Toader',
      phone: '00491739656754',
      identityDocument: id as IdentityDocument,
      address: {
        city: 'Düsseldorf',
        country: 'Germany',
        street: 'Lichtenbroicher Weg',
        number: '2',
        postalCode: '40472',
        state: 'NRW',
        floor: '2'
      }
    };
    this.dialogRef = this.dialogService.open(PersonDataComponent, {
      contentStyle: { overflow: 'auto' },
      maximizable: true,
      draggable: true,
      modal:true,
      resizable: true,
      keepInViewport: true,
      styleClass: "person-data-dialog",
      data: p,
    });

    this.dialogRef.onClose.subscribe(personData => {
      console.error(personData);
      if(personData) {

      }
    });
  }

  public ngOnDestroy() {
    this.dialogRef?.close();
  }
}

class PersonDataGridViewModel {
  constructor(private _underlyingData: PersonData) {

  }
  public get fullname(): string {
    return `${this._underlyingData?.lastName} ${this._underlyingData?.firstName}`;
  }
}
