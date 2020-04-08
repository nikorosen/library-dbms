using System;
using System.Collections.Generic;

namespace library_dbms.Models
{
    public partial class EmployeeLocation
    {
        public int EmployeeId { get; set; }
        public string BuildingNum { get; set; }
        public string RoomNum { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
