using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidationUsing.Web.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? BirthDate { get; set; }

        //Burada IList kullandık çünkü ben şu şekilde erişmek istiyorum customer.Address[0] gibi bana index'li gelsin istiyorum adreslerin. 
        public IList<Address> Addresses { get; set; }
    }
}
