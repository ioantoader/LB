import { Injectable } from '@angular/core';
import { MenuModel } from './../../../layout/model/menu.model';
import { AppMenuItemsService } from './../../../layout/service/app.menu.service';

@Injectable()
export class ClientMenuService extends AppMenuItemsService {
  public get items(): MenuModel[] {
    return [
      {
        label: 'Portofoliu',
        items:
          [
            {
              label: 'Cererile mele',
              routerLink: ['/client/requests']
            }
          ]
      },
      {
        label: 'Servicii',
        items:
          [
            {
              label: 'Infinteaza firma noua',
              routerLink: ['/client/request']
            }
          ]
      }
    ];
  }
}

export function CLIENT_MENU_PROVIDER_FACTORY(
  parentAuthService: ClientMenuService) {
  return parentAuthService || new ClientMenuService();
}

export const CLIENT_MENU_PROVIDER = {

  provide: AppMenuItemsService,
  useFactory: CLIENT_MENU_PROVIDER_FACTORY
};

