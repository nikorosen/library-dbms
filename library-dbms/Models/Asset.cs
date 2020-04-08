using System;
using System.Collections.Generic;

namespace library_dbms.Models
{
    public partial class Asset
    {
        public Asset()
        {
            MaintenanceLog = new HashSet<MaintenanceLog>();
            SuppliedBy = new HashSet<SuppliedBy>();
        }

        public int AssetId { get; set; }
        public string Category { get; set; }
        public string BarcodeNum { get; set; }
        public string Manufacturer { get; set; }
        public string ModelNum { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public int Status { get; set; }
        public string SerialNum { get; set; }

        public virtual AssetLocation AssetLocation { get; set; }
        public virtual AssignedToDep AssignedToDep { get; set; }
        public virtual AssignedToEmp AssignedToEmp { get; set; }
        public virtual ICollection<MaintenanceLog> MaintenanceLog { get; set; }
        public virtual ICollection<SuppliedBy> SuppliedBy { get; set; }
    }
}
