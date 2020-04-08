using System;
using System.Collections.Generic;

namespace library_dbms.Models
{
    public partial class SalesRep
    {
        public int RepId { get; set; }
        public int VendorId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNum { get; set; }

        public virtual Vendor Vendor { get; set; }
    }
}
