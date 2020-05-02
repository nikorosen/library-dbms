using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace library_dbms.Models
{
    public partial class SuppliedBy
    {
        [DisplayName("Vendor Id")]
        public int VendorId { get; set; }
        public int AssetId { get; set; }
        [DisplayName("Purchase Price")]
        public decimal? PurchasePrice { get; set; }
        [DisplayName("Purchase Date")]
        public DateTime? PurchaseDate { get; set; }
    }
}
