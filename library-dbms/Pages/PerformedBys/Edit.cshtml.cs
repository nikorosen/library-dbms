using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using library_dbms.Models;

namespace library_dbms.Pages.PerformedBys
{
    public class EditModel : PageModel
    {
        private readonly library_dbms.Models.InventoryContext _context;

        public EditModel(library_dbms.Models.InventoryContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PerformedBy PerformedBy { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PerformedBy = await _context.PerformedBy
                .Include(p => p.Log).FirstOrDefaultAsync(m => m.EmployeeId == id);

            if (PerformedBy == null)
            {
                return NotFound();
            }
           ViewData["LogId"] = new SelectList(_context.MaintenanceLog, "LogId", "Description");
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

            _context.Attach(PerformedBy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerformedByExists(PerformedBy.EmployeeId))
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

        private bool PerformedByExists(int id)
        {
            return _context.PerformedBy.Any(e => e.EmployeeId == id);
        }
    }
}
