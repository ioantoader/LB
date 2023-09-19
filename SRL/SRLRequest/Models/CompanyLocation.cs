﻿namespace IT.DigitalCompany.Models
{
    public class CompanyLocationContract
    {
        public Int32 DurationInYears { get; set; }
        public Decimal? MonthlyRental { get; set; }

        public Decimal? RentalDeposit { get; set; }

    }
    public class CompanyLocation
    {
        public Guid Id { get; set; }
        public Address Address { get; set; } = new Address();
        public CompanyLocationContract Contract { get; set; } = new CompanyLocationContract();

        public ICollection<Person> Owners { get; set; } = new List<Person>();

    }

    public class CompanyLocationOwners
    {
        public Guid CompanyLocationId { get; set; }
        public Guid OwnerId { get; set; }
    }
}
