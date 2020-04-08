using System;
using System.Collections.Generic;

namespace library_dbms.Models
{
    public partial class DepartmentLocation
    {
        public int DepartmentNum { get; set; }
        public string BuildingNum { get; set; }
        public string RoomNum { get; set; }

        public virtual Department DepartmentNumNavigation { get; set; }
    }
}
