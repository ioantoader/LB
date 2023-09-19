import { Routes, RouterModule } from '@angular/router';
import { ClientLayoutComponent } from './client.layout.component';
import { RequestsComponent } from './components/requests/requests.component';
import { AuthorizeGuard } from '../../../api-authorization/authorize.guard';
import { CompanyRequestComponent } from './components/company-request/company-request.component';

const routes: Routes = [
  {
    path: '', component: ClientLayoutComponent,
    children: [
      {
        path: '', redirectTo: 'requests', pathMatch: 'full'
      },
      {
        path: 'requests', component: RequestsComponent
      },
      {
        path: 'request', component: CompanyRequestComponent
      },
      /*{
        path: 'requests/srl/create', redirectTo: `requests/srl/${crypto.randomUUID()}`
      },*/
      {
        path: 'requests/srl/:companyId', loadChildren: () => import('./components/company-request/srl-company-request/srl-company-request.module').then(m => m.SRLCompanyRequestModule),
        canActivate: [AuthorizeGuard]

      }
    ]
  },
];

export const ClientRoutes = RouterModule.forChild(routes);
