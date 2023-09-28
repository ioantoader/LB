using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace IT.DigitalCompany.Models
{
    public class Address
    {
        public Guid Id { get; set; }
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

        [NotMapped]
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        internal Boolean IsNew
        {
            get
            {
                return this.Id.Equals(Guid.Empty);
            }
        }

    }
}
