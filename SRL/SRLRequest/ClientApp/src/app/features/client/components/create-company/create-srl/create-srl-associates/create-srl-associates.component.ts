import { Component, OnDestroy, OnInit } from '@angular/core';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { AssociateDataComponent } from '../associate-data/associate-data.component';
import { AssociateData } from 'src/app/features/shared/models/associate-data.model';
import { IdentityDocument } from 'src/app/features/shared/models/Identity-document.model';
import { firstValueFrom } from 'rxjs';
import { DataService } from 'src/app/features/shared/services/data.service';
import { ConfirmationService, MessageService } from 'primeng/api';

@Component({
  selector: 'app-create-srl-associates',
  templateUrl: './create-srl-associates.component.html',
  styleUrls: ['./create-srl-associates.component.scss'],
  providers: [DialogService, ConfirmationService, MessageService]
})
export class CreateSrlAssociatesComponent implements OnInit, OnDestroy {

  private dialogRef?: DynamicDialogRef;
  public associates: AssociateGridViewModel[] = [];
  constructor(private dataService: DataService, public dialogService: DialogService, private confirmationService: ConfirmationService, private messageService: MessageService) {

  }

  ngOnInit() {
  }


  public async addAssociate() {
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
    let p: Partial<AssociateData> = {
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

    let associateData = await this.openAssociateDialog(<AssociateData>p);
    if(associateData) {
      associateData = await this.dataService.addAssociate('',associateData);
      this.associates.push(new AssociateGridViewModel(associateData));
      this.associates = [...this.associates];
    }
  }

  public deleteAssociate(associate: AssociateGridViewModel): void {


    this.confirmationService.confirm({
      message: `Sunteti siguri ca doriti sa stergeti asociatul ${associate.fullname}`,
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {this.deleteAssociateInternal(associate);}
    });
  }

  private deleteAssociateInternal(associate: AssociateGridViewModel): void {
    const id = associate.underlyingData.id!;
    this.dataService.deleteAssociate(id);
    this.associates = this.associates.filter(a => a.underlyingData.id !== id);
  }

  public async editAssociate(associate: AssociateGridViewModel): Promise<void> {
    const associateData = await this.openAssociateDialog(associate.underlyingData);
    if(associateData) {
      await this.dataService.updateAssociate(associateData);
      const idx = this.associates.findIndex(a => a?.underlyingData?.id === associateData.id);
      const associate = new AssociateGridViewModel(associateData)
      if(idx >= 0) {
        this.associates[idx] = associate;
      } else {
        this.associates.push(associate);
      }

      this.associates = [...this.associates];
    }

  }

  private async openAssociateDialog(associate: AssociateData): Promise<AssociateData> {
    this.dialogRef = this.dialogService.open(AssociateDataComponent, {
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
}

class AssociateGridViewModel {
  public fullname?: string;
  public cnp?: string;
  constructor(public underlyingData: AssociateData) {
    this.fullname = `${this.underlyingData?.lastName} ${this.underlyingData?.firstName}`;
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
