using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace library_dbms.Models
{
    public partial class LtsStaff
    {
        public int EmployeeId { get; set; }
        [DisplayName("Labor Cost")]
        public decimal? LaborCost { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
