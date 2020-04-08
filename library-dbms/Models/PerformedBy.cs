using System;
using System.Collections.Generic;

namespace library_dbms.Models
{
    public partial class PerformedBy
    {
        public int EmployeeId { get; set; }
        public int LogId { get; set; }
        public double? TotalCost { get; set; }

        public virtual LtsStaff Employee { get; set; }
        public virtual MaintenanceLog Log { get; set; }
    }
}
