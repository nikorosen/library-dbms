using System;
using System.Collections.Generic;

namespace library_dbms.Models
{
    public partial class AssetLocation
    {
        public int AssetId { get; set; }
        public string BuildingNum { get; set; }
        public string RoomNum { get; set; }
        public string Notes { get; set; }

        public virtual Asset Asset { get; set; }
    }
}
