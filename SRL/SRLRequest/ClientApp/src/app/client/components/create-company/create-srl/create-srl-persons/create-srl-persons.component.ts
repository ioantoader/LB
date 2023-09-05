import { Component, OnInit } from '@angular/core';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { PersonDataComponent } from '../person-data/person-data.component';

@Component({
  selector: 'app-create-srl-persons',
  templateUrl: './create-srl-persons.component.html',
  styleUrls: ['./create-srl-persons.component.scss'],
  providers: [DialogService]
})
export class CreateSrlPersonsComponent implements OnInit {

  dialogRef: DynamicDialogRef | undefined;
  constructor(public dialogService: DialogService) { }

  ngOnInit() {
  }

  public addPerson() {
    this.dialogRef = this.dialogService.open(PersonDataComponent, {
      width: '70%',
      height: '70%',
      contentStyle: { overflow: 'auto' },
      maximizable: true,
      draggable: true,
      modal:true,
      resizable: true,
      keepInViewport: true
    });
  }
}
