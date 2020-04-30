using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace library_dbms.Models
{
    public partial class MaintenanceLog
    {
        public MaintenanceLog()
        {
            PerformedBy = new HashSet<PerformedBy>();
        }

        public int LogId { get; set; }
        public int AssetId { get; set; }
        [DisplayName("Date Performed")]
        public DateTime DatePerformed { get; set; }
        [DisplayName("Hours Logged")]
        public int? HoursLogged { get; set; }
        public string Description { get; set; }

        public virtual Asset Asset { get; set; }
        public virtual ICollection<PerformedBy> PerformedBy { get; set; }
    }
}
