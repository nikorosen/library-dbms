using System;
using System.Collections.Generic;

namespace library_dbms.Models
{
    public partial class LtsStaff
    {
        public LtsStaff()
        {
            PerformedBy = new HashSet<PerformedBy>();
        }

        public int EmployeeId { get; set; }
        public double? LaborCost { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual ICollection<PerformedBy> PerformedBy { get; set; }
    }
}
