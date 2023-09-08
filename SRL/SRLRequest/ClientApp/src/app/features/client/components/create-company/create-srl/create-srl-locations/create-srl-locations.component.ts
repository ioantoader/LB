import { Component, OnDestroy, OnInit } from '@angular/core';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { firstValueFrom } from 'rxjs';
import { CompanyLocation } from 'src/app/features/shared/models/company-location.model';
import { LocationComponent } from '../location/location.component';

@Component({
  selector: 'app-create-srl-location',
  templateUrl: './create-srl-locations.component.html',
  styleUrls: ['./create-srl-locations.component.scss']
})
export class CreateSrlLocationsComponent implements OnInit, OnDestroy {
  dialogRef?: DynamicDialogRef;
  constructor(private _dialogService: DialogService) { }

  ngOnDestroy(): void {
    this.dialogRef?.close();
  }

  ngOnInit() {
  }

  public async addLocation() {
    let location = await this.openAssociateDialog();
  }

  private async openAssociateDialog(location?: CompanyLocation): Promise<CompanyLocation> {
    this.dialogRef = this._dialogService.open(LocationComponent, {
      contentStyle: { overflow: 'auto' },
      maximizable: true,
      draggable: true,
      modal:true,
      resizable: true,
      keepInViewport: true,
      styleClass: "company-location-dialog",
      data: Location,
    });

    return await firstValueFrom(this.dialogRef.onClose);

  }

}
