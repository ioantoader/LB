import { Injectable } from '@angular/core';
import { MenuModel } from '../model/menu.model';

@Injectable()
export abstract class AppMenuItemsService {

  constructor() { }

  public abstract get items(): MenuModel[];

}


