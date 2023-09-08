import { Address } from "./address.model";
import { IdentityDocumentBasic } from "./Identity-document.model";

export interface CompanyLocationContract {
  durationInYears: number;
  monthlyRental?: number;
  rentalDeposit?: number;
}
export interface CompanyLocationOwner {
  identityDocument: IdentityDocumentBasic;
}
export interface CompanyLocation {
  address: Address;
  locationContract: CompanyLocationContract;
  owners: CompanyLocationOwner[];
}
