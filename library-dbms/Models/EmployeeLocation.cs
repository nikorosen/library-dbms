using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace library_dbms.Models
{
    public partial class EmployeeLocation
    {
        public int EmployeeId { get; set; }
        [DisplayName("Building #")]
        public string BuildingNum { get; set; }
        [DisplayName("Room #")]
        public string RoomNum { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
