import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { CompanyRequest } from '../../../../shared/models/company-request.model';
import { AuthorizeService } from '../../../../../../api-authorization/authorize.service';
import { firstValueFrom, lastValueFrom } from 'rxjs';
import { CompanyRequestService } from '../../../../shared/models/company-request.service';
@Component({
  selector: 'app-srl-company-request',
  templateUrl: './srl-company-request.component.html',
  styleUrls: ['./srl-company-request.component.scss'],
  providers: [CompanyRequestService]
})
export class SRLCompanyRequestComponent implements OnInit {

  company?: CompanyRequest;
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
