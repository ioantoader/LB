export interface IDCardBasic {
  serial:           string;
  number:           string;
  cnp:              string;
}
export interface IDCard extends IDCardBasic{
  birthCountry:     string;
  birthState:       string;
  birthCity:        string;
  citizenship:      string;
  issueDate:        Date;
  expirationDate:   Date;
  issueBy:          string;
  birthDate:        Date;

}
