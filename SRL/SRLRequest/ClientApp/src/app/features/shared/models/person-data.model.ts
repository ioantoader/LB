
import { IDCard } from "./Identity-document.model";
import { Address } from "./address.model";
import { Contact } from "./contact.model";

export interface PersonData {

  id?:                string;
  contact:            Contact;
  identityDocument:   IDCard;
  address?:           Address;
}
