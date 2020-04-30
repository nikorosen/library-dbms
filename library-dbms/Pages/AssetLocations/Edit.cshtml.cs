using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using library_dbms.Models;

namespace library_dbms.Pages.AssetLocations
{
    public class EditModel : PageModel
    {
        private readonly library_dbms.Models.InventoryContext _context;

        public EditModel(library_dbms.Models.InventoryContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AssetLocation AssetLocation { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AssetLocation = await _context.AssetLocation
                .Include(a => a.Asset).FirstOrDefaultAsync(m => m.AssetId == id);

            if (AssetLocation == null)
            {
                return NotFound();
            }
           ViewData["AssetId"] = new SelectList(_context.Asset, "AssetId", "AssetId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(AssetLocation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetLocationExists(AssetLocation.AssetId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AssetLocationExists(int id)
        {
            return _context.AssetLocation.Any(e => e.AssetId == id);
        }
    }
}
