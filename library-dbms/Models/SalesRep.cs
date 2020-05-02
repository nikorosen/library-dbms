using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace library_dbms.Models
{
    public partial class SalesRep
    {
        public int RepId { get; set; }
        public int VendorId { get; set; }
        public string Email { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Phone Num")]
        public string PhoneNum { get; set; }
        public string Ext { get; set; }
        public string Title { get; set; }

        public virtual Vendor Vendor { get; set; }
    }
}
