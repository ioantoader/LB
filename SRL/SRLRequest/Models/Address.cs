namespace SRLRequest.Models
{
    public class Address
    {
        public String Country { get; set; }
        public String City { get; set; }
        public String PostalCode { get; set; }
        public String Number { get; set; }
        public String Street { get; set; }
        public String? State {  get; set; }
        public String? Block { get; set; }
        public String? Stair { get; set; }
        public String? Floor { get; set; }
        public String? Apartment { get; set; }

    }
}
