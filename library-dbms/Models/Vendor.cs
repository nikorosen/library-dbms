using System;
using System.Collections.Generic;

namespace library_dbms.Models
{
    public partial class Vendor
    {
        public Vendor()
        {
            SalesRep = new HashSet<SalesRep>();
            SuppliedBy = new HashSet<SuppliedBy>();
        }

        public int VendorId { get; set; }
        public string Address { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNum { get; set; }
        public string Website { get; set; }

        public virtual ICollection<SalesRep> SalesRep { get; set; }
        public virtual ICollection<SuppliedBy> SuppliedBy { get; set; }
    }
}
