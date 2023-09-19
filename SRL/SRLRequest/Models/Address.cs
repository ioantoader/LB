namespace IT.DigitalCompany.Models
{
    public class Address
    {
        public String Country { get; set; } = null!;
        public String City { get; set; } = null!;
        public String PostalCode { get; set; } = null!;
        public String Number { get; set; } = null!;
        public String Street { get; set; } = null!;
        public String? State {  get; set; }
        public String? Block { get; set; }
        public String? Stair { get; set; }
        public String? Floor { get; set; }
        public String? Apartment { get; set; }

    }
}
