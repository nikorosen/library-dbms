using System;
using System.Collections.Generic;

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
        public DateTime DatePerformed { get; set; }
        public double? HoursLogged { get; set; }
        public string Description { get; set; }

        public virtual Asset Asset { get; set; }
        public virtual ICollection<PerformedBy> PerformedBy { get; set; }
    }
}
