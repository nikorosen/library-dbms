using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using library_dbms.Models;

namespace library_dbms.Pages.SalesReps
{
    public class DeleteModel : PageModel
    {
        private readonly library_dbms.Models.InventoryContext _context;

        public DeleteModel(library_dbms.Models.InventoryContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SalesRep = await _context.SalesRep.FindAsync(id);

            if (SalesRep != null)
            {
                _context.SalesRep.Remove(SalesRep);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
