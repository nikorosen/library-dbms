using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using library_dbms.Models;

namespace library_dbms.Pages.MaintenanceLogs
{
    public class IndexModel : PageModel
    {
        private readonly library_dbms.Models.InventoryContext _context;

        public IndexModel(library_dbms.Models.InventoryContext context)
        {
            _context = context;
        }

        public IList<MaintenanceLog> MaintenanceLog { get;set; }
        
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task OnGetAsync()
        {
            var maintenanceLogs = from a in _context.MaintenanceLog.Include(u => u.Asset) select a;

            if (!string.IsNullOrEmpty(SearchString))
            {
                maintenanceLogs = maintenanceLogs.Where(x => x.AssetId == Int32.Parse(SearchString));
            }

            MaintenanceLog = await maintenanceLogs.ToListAsync();
        }
    }
}
