import { Routes, RouterModule } from '@angular/router';
import { ClientLayoutComponent } from './client.layout.component';
import { RequestsComponent } from './components/requests/requests.component';
import { CreateCompanyComponent } from './components/create-company/create-company.component';
import { CreateSrlComponent } from './components/create-company/create-srl/create-srl.component';

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
      },
      {
        path: 'requests/srl/create', loadChildren: () => import('./components/create-company/create-srl/create-srl.module').then(m => m.CreateSrlModule)
      }
    ]
  },
];

export const ClientRoutes = RouterModule.forChild(routes);
