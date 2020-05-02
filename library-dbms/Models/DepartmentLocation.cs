using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace library_dbms.Models
{
    public partial class DepartmentLocation
    {
        [DisplayName("Department Num")]
        public int DepartmentNum { get; set; }
        [DisplayName("Building Num")]
        public string BuildingNum { get; set; }
        [DisplayName("Room Num")]
        public string RoomNum { get; set; }
        
        [DisplayName("Department Name")]
        public virtual Department DepartmentNumNavigation { get; set; }
    }
}
