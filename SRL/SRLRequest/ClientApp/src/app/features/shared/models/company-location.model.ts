import { Address } from "./address.model";
import { PersonData } from "./person-data.model";

export interface CompanyLocationContract {
  durationInYears: number;
  monthlyRental?: number;
  rentalDeposit?: number;
}

export interface CompanyLocation {
  id?: string;
  address: Address;
  contract: CompanyLocationContract;
  owners: Partial<PersonData>[];
}
