using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace FluentValidationUsing.Web.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }
        public int DoorNumber { get; set; }
        public string PostalCode { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
