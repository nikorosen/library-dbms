using System;
using System.Collections.Generic;

namespace library_dbms.Models
{
    public partial class AssignedToEmp
    {
        public int AssetId { get; set; }
        public int EmployeeId { get; set; }

        public virtual Asset Asset { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
