using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using library_dbms.Models;

namespace library_dbms.Pages.SalesReps
{
    public class EditModel : PageModel
    {
        private readonly library_dbms.Models.InventoryContext _context;

        public EditModel(library_dbms.Models.InventoryContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SalesRep SalesRep { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SalesRep = await _context.SalesRep
                .Include(s => s.Vendor).FirstOrDefaultAsync(m => m.RepId == id);

            if (SalesRep == null)
            {
                return NotFound();
            }
           ViewData["VendorId"] = new SelectList(_context.Vendor, "VendorId", "CompanyName");
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

            _context.Attach(SalesRep).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesRepExists(SalesRep.RepId))
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

        private bool SalesRepExists(int id)
        {
            return _context.SalesRep.Any(e => e.RepId == id);
        }
    }
}
