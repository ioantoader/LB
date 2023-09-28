export interface Address {
  id?:          string | undefined;
  country:      string;
  city:         string;
  postalCode:   string;
  number:       string;
  street?:      string;
  state?:       string;
  block?:       string;
  stair?:       string;
  floor?:       string;
  apartment?:   string;
}
