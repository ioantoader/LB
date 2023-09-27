import { Component, OnDestroy, OnInit } from '@angular/core';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { PersonData } from 'src/app/features/shared/models/person-data.model';
import { IDCard } from 'src/app/features/shared/models/Identity-document.model';
import { firstValueFrom } from 'rxjs';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Contact } from 'src/app/features/shared/models/contact.model';
import { CompanyAssociateComponent } from '../company-associate/company-associate.component';
import { CompanyRequestService } from '../../../../../shared/models/company-request.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-company-associates',
  templateUrl: './company-associates.component.html',
  styleUrls: ['./company-associates.component.scss'],
  providers: [DialogService]
})
export class CompanyAssociatesComponent implements OnInit, OnDestroy {

  private dialogRef?: DynamicDialogRef;
  public associates: AssociateGridViewModel[] = [];
  constructor(private _companyRequestService: CompanyRequestService,
    public dialogService: DialogService,
    private _confirmationService: ConfirmationService,
    private _messageService: MessageService,
    private _router: Router,
    private _route: ActivatedRoute) {

    this._companyRequestService.companyRequest$.subscribe(r => {
      if (r) {
        this.associates = (r?.associates ?? [])
          .map(a => new AssociateGridViewModel(a));
      }
    });
  }

  ngOnInit() {
  }


  public async addAssociate() {
    let id: Partial<IDCard> = {
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
      contact: {
        firstName: 'Ioan',
        lastName: 'Toader',
        phoneNumber: '00491739656754',
      } as Contact,
      identityDocument: id as IDCard,
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

    let associateData = await this.openAssociateDialog(<PersonData>p);
    if (associateData) {
      associateData = await this._companyRequestService.addAssociate(associateData);
      this.associates.push(new AssociateGridViewModel(associateData));
      this.associates = [...this.associates];
    }
  }

  public deleteAssociate(associate: AssociateGridViewModel): void {
    this._confirmationService.confirm({
      message: `Sunteti siguri ca doriti sa stergeti asociatul ${associate.fullname}`,
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {this.deleteAssociateInternal(associate);}
    });
  }

  private deleteAssociateInternal(associate: AssociateGridViewModel): void {
    const id = associate.underlyingData.id!;
    this._companyRequestService.deleteAssociate(id);
    this.associates = this.associates.filter(a => a.underlyingData?.id?.toLowerCase() !== id?.toUpperCase());
  }

  public async editAssociate(associate: AssociateGridViewModel): Promise<void> {
    let associateData = await this.openAssociateDialog(associate.underlyingData);
    if (associateData) {
      associateData = await this._companyRequestService.updateAssociate(associateData);
      const idx = this.associates.findIndex(a => a?.underlyingData?.id?.toUpperCase() === associateData!.id?.toUpperCase());
      const associate = new AssociateGridViewModel(associateData)
      if(idx >= 0) {
        this.associates[idx] = associate;
      } else {
        this.associates.push(associate);
      }

      this.associates = [...this.associates];
    }

  }

  private async openAssociateDialog(associate?: PersonData): Promise<PersonData | null | undefined> {
    this.dialogRef = this.dialogService.open(CompanyAssociateComponent, {
      contentStyle: { overflow: 'auto' },
      maximizable: true,
      draggable: true,
      modal:true,
      resizable: true,
      keepInViewport: true,
      styleClass: "associate-data-dialog",
      data: associate,
    });

    return await firstValueFrom(this.dialogRef.onClose);

  }
  public ngOnDestroy() {
    this.dialogRef?.close();
  }

  public prevPage() {
    this._companyRequestService.gotoCompanyContact(this._router, this._route);
  }

  public nextPage() {
    this._companyRequestService.gotoCompanyLocations(this._router, this._route);
  }

}

class AssociateGridViewModel {
  public fullname?: string;
  public cnp?: string;
  constructor(public underlyingData: PersonData) {
    this.fullname = `${this.underlyingData?.contact?.lastName} ${this.underlyingData?.contact?.firstName}`;
    this.cnp = this.underlyingData?.identityDocument?.cnp;
  }
  /*
  public get fullname(): string {
    return `${this._underlyingData?.lastName} ${this._underlyingData?.firstName}`;
  }
  public get cnp(): string {
    return this._underlyingData?.identityDocument?.cnp;
  }*/
}
