namespace SRLRequest.Models
{
    public class CompanyRequest
    {
        public Guid Id { get; set; }
        public Contact? Contact { get; set; }
        public ICollection<Person> Associates { get; set; } = new List<Person>();

        public ICollection<CompanyLocation> Locations { get; set; } = new List<CompanyLocation>();
    }
}
