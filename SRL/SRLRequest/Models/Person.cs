using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace IT.DigitalCompany.Models
{
    public enum PersonType
    {
        Natural = 0,
        Legal = 1,
    }
    public class Person
    {
        public Guid Id { get; set; }
        public PersonType Type { get; set; } = PersonType.Natural;

        //Company Registration Number
        public String? CRN { get; set; }

        public Contact Contact { get; set; } = new Contact();

        public IdentityDocument IdentityDocument { get; set; } = new IdentityDocument();
        public Address Address { get; set; } = new Address();

    }
}
