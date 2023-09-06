import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-create-srl',
  templateUrl: './create-srl.component.html',
  styleUrls: ['./create-srl.component.scss']
})
export class CreateSrlComponent implements OnInit {

  public items: MenuItem[] = [];
  constructor() { }

  ngOnInit() {
    this.items = [
      {
        label: 'Date de contact',
        routerLink: 'contact'
      },
      {
        label: 'Persoanele din firma',
        routerLink: 'persons'
      },
      {
        label: 'Sediul Firmei',
        routerLink: 'location'
      },
      {
        label: 'Activitati autorizate',
        routerLink: 'activities'
      },
      {
        label: 'Denumirea Firmei',
        routerLink: 'names'
      }
    ];
  }

}
