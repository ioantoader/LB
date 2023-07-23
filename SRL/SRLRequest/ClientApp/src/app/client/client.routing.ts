import { Routes, RouterModule } from '@angular/router';
import { ClientLayoutComponent } from './client.layout.component';
import { RequestsComponent } from './components/requests/requests.component';
import { CreateCompanyComponent } from './components/create-company/create-company.component';

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
        path: 'request', component: CreateCompanyComponent
      }
    ]
  },
];

export const ClientRoutes = RouterModule.forChild(routes);
