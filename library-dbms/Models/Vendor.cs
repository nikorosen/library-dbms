using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace library_dbms.Models
{
    public partial class Vendor
    {
        public Vendor()
        {
            SalesRep = new HashSet<SalesRep>();
        }

        [DisplayName("Vendor Id")]
        public int VendorId { get; set; }
        public string Address { get; set; }
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }
        [DisplayName("Phone #")]
        public string PhoneNum { get; set; }
        public string Website { get; set; }

        public virtual ICollection<SalesRep> SalesRep { get; set; }
    }
}
