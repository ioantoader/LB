import { CompanyLocation } from "./company-location.model";
import { CompanyNames } from "./companyNames.model";
import { Contact } from "./contact.model";
import { PersonData } from "./person-data.model";

export interface CompanyRequest {
  id?:          string;
  contact?:     Contact;
  associates?:  PersonData[];
  locations?:   CompanyLocation[];
  names?:       CompanyNames;
}
