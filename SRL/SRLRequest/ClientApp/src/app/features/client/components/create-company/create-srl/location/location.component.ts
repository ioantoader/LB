import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { DialogService, DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { CompanyLocation } from 'src/app/features/shared/models/company-location.model';
import { LocationOwnerComponent } from './location-owner/location-owner.component';
import { PersonData } from 'src/app/features/shared/models/person-data.model';
import { firstValueFrom } from 'rxjs';
import { ConfirmationService } from 'primeng/api';

@Component({
  selector: 'app-location',
  templateUrl: './location.component.html',
  styleUrls: ['./location.component.scss'],
  providers: [DialogService]
})
export class LocationComponent implements OnInit, OnDestroy {
  public location?: CompanyLocation;
  public locationOwners!: Partial<PersonData>[];
  public locationFormGroup!: FormGroup;
  private childDialogRef?: DynamicDialogRef;
  constructor(fb: FormBuilder,
              private _dialogRef: DynamicDialogRef,
              private _dialogService: DialogService,
              private _dialogConfig: DynamicDialogConfig,
              private confirmationService: ConfirmationService) {
    this.location = _dialogConfig.data;
    this.locationOwners = this.location?.owners?? [];
    this.locationFormGroup = fb.group({
      'id': [this.location?.id],
      'address': fb.group(
        {
          'country': this.location?.address?.country,
          'state': this.location?.address?.state,
          'city': this.location?.address?.city,
          'street': this.location?.address?.street,
          'number': this.location?.address?.number,
          'postalCode': this.location?.address?.postalCode,
          'block': this.location?.address?.block,
          'stair': this.location?.address?.stair,
          'floor': this.location?.address?.floor,
          'apartment': this.location?.address?.apartment,
        }),
        'contract': fb.group(
          {
            'durationInYears': this.location?.contract?.durationInYears,
            'monthlyRental': this.location?.contract?.monthlyRental,
            'rentalDeposit': this.location?.contract?.rentalDeposit,
          }),
    })
  }
  ngOnDestroy(): void {
    this.childDialogRef?.close();
  }

  ngOnInit() {
  }


  public async addOwner() {
    const newOwner = await this.openLocationOwnerDialog();
    if(newOwner) {
      newOwner.id = crypto.randomUUID();
      this.locationOwners.push(newOwner);
      this.locationOwners = [...this.locationOwners];
    }
  }

  public async editOwner(locationOwner: Partial<PersonData>) {
    const owner = await this.openLocationOwnerDialog(locationOwner)
    if(owner) {
      const idx = this.locationOwners.findIndex(o => o.id?.toUpperCase() === owner.id?.toUpperCase());
      if(idx >= 0) {
        this.locationOwners[idx] = owner;
        this.locationOwners = [...this.locationOwners];
      }
    }
  }

  public deleteOwner(owner: Partial<PersonData>): void {
    this.confirmationService.confirm({
      message: `Sunteti siguri ca doriti sa stergeti proprietarul ${owner.contact?.lastName} ${owner.contact?.firstName}`,
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {this.deleteOwnerInternal(owner);}
    });
  }

  private deleteOwnerInternal(owner: Partial<PersonData>): void {
    const id = owner.id!;
    this.locationOwners = this.locationOwners.filter(o => o.id?.toUpperCase() !== id?.toUpperCase());
  }


  private async openLocationOwnerDialog(locationOwner?: Partial<PersonData>): Promise<Partial<PersonData>> {
    this.childDialogRef = this._dialogService.open(LocationOwnerComponent, {
      contentStyle: { overflow: 'auto' },
      maximizable: true,
      draggable: true,
      modal:true,
      resizable: true,
      keepInViewport: true,
      styleClass: "location-owner-dialog",
      data: locationOwner,
      autoZIndex: true
    });

    return await firstValueFrom(this.childDialogRef.onClose);

  }

  public save() {
    var t: CompanyLocation = this.locationFormGroup.value;
    t.owners = this.locationOwners;
    this.location = t;
    this._dialogRef.close(this.location);

  }

}
