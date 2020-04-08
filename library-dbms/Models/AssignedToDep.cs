using System;
using System.Collections.Generic;

namespace library_dbms.Models
{
    public partial class AssignedToDep
    {
        public int AssetId { get; set; }
        public int DepartmentNum { get; set; }

        public virtual Asset Asset { get; set; }
        public virtual Department DepartmentNumNavigation { get; set; }
    }
}
