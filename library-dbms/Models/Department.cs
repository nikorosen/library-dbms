using System;
using System.Collections.Generic;

namespace library_dbms.Models
{
    public partial class Department
    {
        public Department()
        {
            AssignedToDep = new HashSet<AssignedToDep>();
            Employee = new HashSet<Employee>();
        }

        public int DepartmentNum { get; set; }
        public string DepartmentName { get; set; }
        public string PhoneNum { get; set; }

        public virtual DepartmentLocation DepartmentLocation { get; set; }
        public virtual ICollection<AssignedToDep> AssignedToDep { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
    }
}
