namespace IT.DigitalCompany.Models
{
    public class CompanyRegistrationRequest
    {
        public Guid Id { get; set; }

        public String UserId { get; set; } = null!;
        public Contact? Contact { get; set; }
        public ICollection<Person> Associates { get; set; } = new List<Person>();

        public ICollection<CompanyLocation> Locations { get; set; } = new List<CompanyLocation>();
    }

    public class CompanyRequestAssociates
    {
        public Guid CompanyRequestId { get; set; }
        public Guid AssociateId { get; set; }
    }
}
