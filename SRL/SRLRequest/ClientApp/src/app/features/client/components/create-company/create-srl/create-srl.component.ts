import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { Company } from './../../../../shared/models/company.model';
import { AuthorizeService } from '../../../../../../api-authorization/authorize.service';
import { firstValueFrom, lastValueFrom } from 'rxjs';
@Component({
  selector: 'app-create-srl',
  templateUrl: './create-srl.component.html',
  styleUrls: ['./create-srl.component.scss']
})
export class CreateSrlComponent implements OnInit {

  company?: Company;
  public items: MenuItem[] = [];
  constructor(private _auth: AuthorizeService) {    
  }

  async ngOnInit() {
    this.items = [
      {
        label: 'Date de contact',
        routerLink: 'contact'
      },
      {
        label: 'Persoanele din firma',
        routerLink: 'associates'
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
    const u = await firstValueFrom(this._auth.getUser());
    console.error(u);
  }

}
