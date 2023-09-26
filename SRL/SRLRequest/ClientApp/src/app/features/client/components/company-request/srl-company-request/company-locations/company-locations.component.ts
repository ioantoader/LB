import { Component, OnDestroy, OnInit } from '@angular/core';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { firstValueFrom } from 'rxjs';
import { CompanyLocation, CompanyLocationContract } from 'src/app/features/shared/models/company-location.model';
import { LocationComponent } from '../location/location.component';
import { ConfirmationService } from 'primeng/api';
import { CompanyRequestService } from '../../../../../shared/models/company-request.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Address } from '../../../../../shared/models/address.model';

@Component({
  selector: 'app-company-locations',
  templateUrl: './company-locations.component.html',
  styleUrls: ['./company-locations.component.scss'],
  providers: [DialogService]
})
export class CompanyLocationsComponent implements OnInit, OnDestroy {

  dialogRef?: DynamicDialogRef;
  public locations: CompanyLocation[] = [];
  constructor(private _companyRequestService: CompanyRequestService,
              private _dialogService: DialogService,
              private _confirmationService: ConfirmationService,
              private _router: Router,
              private _route: ActivatedRoute
  ) {
    this.locations = _companyRequestService.companyRequest?.locations?? [];
  }

  ngOnDestroy(): void {
    this.dialogRef?.close();
  }

  ngOnInit() {

  }

  public async addLocation() {
    let a: Partial<Address> = {
      country: 'Germany',
      street: 'Lichtenbroicher Weg',
      city: 'Düsseldorf',
      number: '2',
      postalCode: '40475'
    }
    let c: Partial<CompanyLocationContract> = {
      durationInYears: 10,
    }
    let location: CompanyLocation | null | undefined = {
      address: <Address>a,
      contract: <CompanyLocationContract>c,
      owners: []

    };
    location = await this.openLocationDialog(location);
    if (location) {
      await this._companyRequestService.addLocation(location)
      this.locations = this.locations.concat(location);
    }
  }

  public async editLoaction(location: CompanyLocation) {
    const editLocation = await this.openLocationDialog(location);
    if(editLocation) {
      const idx = this.locations.findIndex(l => l.id?.toUpperCase() === location.id?.toUpperCase());
      if(idx >= 0) {
        this.locations[idx] = editLocation;
        this.locations = [...this.locations];
      }
    }
  }

  public async deleteLocation(location: CompanyLocation) {
    this._confirmationService.confirm({
      message: `Sunteti siguri ca doriti sa stergeti locatia?`,
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {this.deleteLocationInternal(location);}
    });

  }

  public async deleteLocationInternal(location: CompanyLocation) {
    this.locations = this.locations.filter(l => l.id?.toUpperCase() !== location.id?.toUpperCase());
  }
  private async openLocationDialog(location?: CompanyLocation): Promise<CompanyLocation | null | undefined> {
    this.dialogRef = this._dialogService.open(LocationComponent, {
      contentStyle: { overflow: 'auto' },
      maximizable: true,
      draggable: true,
      modal:true,
      resizable: true,
      keepInViewport: true,
      styleClass: "company-location-dialog",
      data: location,
      autoZIndex: true,
    });

    return await firstValueFrom(this.dialogRef.onClose);

  }

  public prevPage() {
    this._companyRequestService.gotoCompanyAssociates(this._router, this._route);
  }

  public nextPage() {

  }
}
