using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace library_dbms.Models
{
    public partial class AssetLocation
    {
        public int AssetId { get; set; }
        [DisplayName("Building #")]
        public string BuildingNum { get; set; }
        [DisplayName("Room #")]
        public string RoomNum { get; set; }
        public string Notes { get; set; }

        public virtual Asset Asset { get; set; }
    }
}
