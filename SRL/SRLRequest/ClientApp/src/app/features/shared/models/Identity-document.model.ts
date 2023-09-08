export interface IdentityDocumentBasic {
  serial:           string;
  number:           string;
  cnp:              string;
}
export interface IdentityDocument extends IdentityDocumentBasic{
  birthCountry:     string;
  birthState:       string;
  birthCity:        string;
  citizenship:      string;
  issueDate:        Date;
  expirationDate:   Date;
  issueBy:          string;
  birthDate:        Date;

}
