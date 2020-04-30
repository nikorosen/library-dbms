using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace library_dbms.Models
{
    public partial class AssignedToDep
    {
        public int AssetId { get; set; }
        
        [DisplayName("Department #")]
        public int DepartmentNum { get; set; }

        public virtual Asset Asset { get; set; }
        
        [DisplayName("Department Name")]
        public virtual Department DepartmentNumNavigation { get; set; }
    }
}
