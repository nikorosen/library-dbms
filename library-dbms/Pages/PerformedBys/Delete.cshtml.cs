using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using library_dbms.Models;

namespace library_dbms.Pages.PerformedBys
{
    public class DeleteModel : PageModel
    {
        private readonly library_dbms.Models.InventoryContext _context;

        public DeleteModel(library_dbms.Models.InventoryContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PerformedBy = await _context.PerformedBy.FindAsync(id);

            if (PerformedBy != null)
            {
                _context.PerformedBy.Remove(PerformedBy);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
