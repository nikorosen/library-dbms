using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace library_dbms.Models
{
    public class AssignedTo
    {
        public AssignedToEmp AssignedToEmp { get; set; }
        public AssignedToDep AssignedToDep { get; set; }

        public AssignedTo(){
            AssignedToEmp = null;
            AssignedToDep = null;
        }
    }
}
