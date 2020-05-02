using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace library_dbms.Models
{
    public partial class Employee
    {
        public Employee()
        {
            AssignedToEmp = new HashSet<AssignedToEmp>();
            EmployeeLocation = new HashSet<EmployeeLocation>();
        }

        public int EmployeeId { get; set; }
        [DisplayName("Departmnet Num")]
        public int? DepartmentNum { get; set; }
        public string Email { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Phone Num")]
        public string PhoneNum { get; set; }
        [DisplayName("Department Name")]
        public virtual Department DepartmentNumNavigation { get; set; }
        public virtual LtsStaff LtsStaff { get; set; }
        public virtual ICollection<AssignedToEmp> AssignedToEmp { get; set; }
        public virtual ICollection<EmployeeLocation> EmployeeLocation { get; set; }
    }
}
