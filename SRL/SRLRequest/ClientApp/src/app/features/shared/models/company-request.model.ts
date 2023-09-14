import { CompanyLocation } from "./company-location.model";
import { Contact } from "./contact.model";
import { PersonData } from "./person-data.model";

export interface CompanyRequest {
  id:           string;
  conatct:      Contact;
  associates:   PersonData[];
  locations:    CompanyLocation[];

}
