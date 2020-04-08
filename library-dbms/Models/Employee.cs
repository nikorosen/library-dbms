using System;
using System.Collections.Generic;

namespace library_dbms.Models
{
    public partial class Employee
    {
        public Employee()
        {
            AssignedToEmp = new HashSet<AssignedToEmp>();
        }

        public int EmployeeId { get; set; }
        public int DepartmentNum { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNum { get; set; }

        public virtual Department DepartmentNumNavigation { get; set; }
        public virtual EmployeeLocation EmployeeLocation { get; set; }
        public virtual LtsStaff LtsStaff { get; set; }
        public virtual ICollection<AssignedToEmp> AssignedToEmp { get; set; }
    }
}
