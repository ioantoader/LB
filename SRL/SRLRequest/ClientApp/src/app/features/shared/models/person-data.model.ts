
import { IdentityDocument } from "./Identity-document.model";
import { Address } from "./address.model";

export interface PersonData {
  firstName: string;
  lastName: string;
  phone: string;
  identityDocument: IdentityDocument;
  address: Address;
}
