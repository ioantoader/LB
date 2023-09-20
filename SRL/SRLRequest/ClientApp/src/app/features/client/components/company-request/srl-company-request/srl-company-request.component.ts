import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { CompanyRequest } from '../../../../shared/models/company-request.model';
import { AuthorizeService } from '../../../../../../api-authorization/authorize.service';
import { filter, firstValueFrom, take} from 'rxjs';
import { CompanyRequestService } from '../../../../shared/models/company-request.service';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
@Component({
  selector: 'app-srl-company-request',
  templateUrl: './srl-company-request.component.html',
  styleUrls: ['./srl-company-request.component.scss'],
  providers: [CompanyRequestService]
})
export class SRLCompanyRequestComponent implements OnInit {
  
  public items: MenuItem[] = [];
  constructor(private _companyRegisterRequestService: CompanyRequestService,
    private _route: ActivatedRoute,
    private _router: Router,
    private _auth: AuthorizeService) {
    this.items = _companyRegisterRequestService.steps;
    this.loadCompanyRegistrationRequest();
  }

  async ngOnInit() {    
    const u = await firstValueFrom(this._auth.getUser());    
  }

  private async loadCompanyRegistrationRequest() {    
    const requestId = this._route.snapshot.paramMap.get('companyId');
    var p = new Promise<CompanyRequest | undefined | null>((resolve, reject) => {
      this._router.events
        .pipe(
          filter((event) => event instanceof NavigationEnd)
          , take(1)
        ).subscribe({ next: e => {
          const s = this._router.getCurrentNavigation()?.extras?.state;
          let cr: CompanyRequest | undefined | null = undefined;
          if (s) {
            cr = s[requestId!];
          }
          resolve(cr);                    
        },
          error: err => reject(err)          
        });

    })    
    let request = await p;
    if (!request) {
      request = {};
    }

    this._companyRegisterRequestService.companyRequest = request;
    
  }
}
