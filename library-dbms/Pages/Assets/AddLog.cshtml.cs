using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using library_dbms.Models;
using Microsoft.EntityFrameworkCore;

namespace library_dbms.Pages.MaintenanceLogs
{
    public class AddLogModel : PageModel
    {
        private readonly library_dbms.Models.InventoryContext _context;

        public AddLogModel(library_dbms.Models.InventoryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id)
        {
            Asset = _context.Asset.FirstOrDefault(u => u.AssetId == id);

            var maintenanceLogs = from a in _context.MaintenanceLog.Include(u => u.Asset) select a;
            maintenanceLogs = maintenanceLogs.Where(x => x.AssetId == id);

            MaintenanceLogList = maintenanceLogs.ToList();

            ViewData["AssetId"] = new SelectList(_context.Asset, "AssetId", "AssetId");
            return Page();
        }

        [BindProperty]
        public MaintenanceLog MaintenanceLog { get; set; }
        [BindProperty]
        public Asset Asset { get; set; }
        public IList<MaintenanceLog> MaintenanceLogList { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            MaintenanceLog.AssetId = (int)id;
            _context.MaintenanceLog.Add(MaintenanceLog);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Assets/AddLog", new { id = MaintenanceLog.AssetId });
        }
    }
}
