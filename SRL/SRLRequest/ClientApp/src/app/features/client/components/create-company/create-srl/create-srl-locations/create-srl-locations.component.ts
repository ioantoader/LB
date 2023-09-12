import { Component, OnDestroy, OnInit } from '@angular/core';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { firstValueFrom } from 'rxjs';
import { CompanyLocation } from 'src/app/features/shared/models/company-location.model';
import { LocationComponent } from '../location/location.component';
import { ConfirmationService } from 'primeng/api';

@Component({
  selector: 'app-create-srl-location',
  templateUrl: './create-srl-locations.component.html',
  styleUrls: ['./create-srl-locations.component.scss'],
  providers: [DialogService]
})
export class CreateSrlLocationsComponent implements OnInit, OnDestroy {

  dialogRef?: DynamicDialogRef;
  public locations: CompanyLocation[] = [];
  constructor(private _dialogService: DialogService, private confirmationService: ConfirmationService) { }

  ngOnDestroy(): void {
    this.dialogRef?.close();
  }

  ngOnInit() {

  }

  public async addLocation() {
    let location = await this.openLocationDialog();
    if(location) {
      location.id = crypto.randomUUID();
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
    this.confirmationService.confirm({
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

}
