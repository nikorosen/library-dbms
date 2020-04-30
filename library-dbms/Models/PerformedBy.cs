using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace library_dbms.Models
{
    public partial class PerformedBy
    {
        public int EmployeeId { get; set; }
        public int LogId { get; set; }
        [DisplayName("Total Cost")]
        public decimal? TotalCost { get; set; }

        public virtual MaintenanceLog Log { get; set; }
    }
}
