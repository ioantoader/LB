namespace IT.DigitalCompany.Models
{
    public enum IdentityDocumentType
    {
        IDCard = 1,
        Passport = 2,
    }
    public class IdentityDocument
    {
        public IdentityDocumentType DocumentType { get; set; } = IdentityDocumentType.IDCard;
        public String? Serial { get; set; }
        public String Number { get; set; } = null!;
        public String CNP { get; set; } = null!;
        public String? BirthCountry { get; set; }
        public String? BirthState { get; set; }
        public String? BirthCity { get; set; }
        public String? Citizenship { get; set; }
        public DateTimeOffset? IssueDate { get; set; }
        public DateTimeOffset? ExpirationDate { get; set; }
        public String? IssueBy { get; set; }
        public DateTimeOffset? BirthDate { get; set; }
    }
}
