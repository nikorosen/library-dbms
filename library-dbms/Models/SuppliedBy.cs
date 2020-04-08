using System;
using System.Collections.Generic;

namespace library_dbms.Models
{
    public partial class SuppliedBy
    {
        public int VendorId { get; set; }
        public int AssetId { get; set; }
        public double PurchasePrice { get; set; }
        public DateTime PurchaseDate { get; set; }

        public virtual Asset Asset { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}
