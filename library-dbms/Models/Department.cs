using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace library_dbms.Models
{
    public partial class Department
    {
        public Department()
        {
            AssignedToDep = new HashSet<AssignedToDep>();
            DepartmentLocation = new HashSet<DepartmentLocation>();
            Employee = new HashSet<Employee>();
        }

        [DisplayName("Department Num")]
        public int DepartmentNum { get; set; }
        [DisplayName("Department Name")]
        public string DepartmentName { get; set; }
        [DisplayName("Phone Num")]
        public string PhoneNum { get; set; }

        public virtual ICollection<AssignedToDep> AssignedToDep { get; set; }
        public virtual ICollection<DepartmentLocation> DepartmentLocation { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
    }
}
