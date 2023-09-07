
import { IdentityDocument } from "./Identity-document.model";
import { Address } from "./address.model";

export interface AssociateData {

  id?:                string;
  firstName:          string;
  lastName:           string;
  phone:              string;
  identityDocument:   IdentityDocument;
  address:            Address;
}
