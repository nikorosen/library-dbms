using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using library_dbms.Models;

namespace library_dbms.Pages.AssignedToEmps
{
    public class DeleteModel : PageModel
    {
        private readonly library_dbms.Models.InventoryContext _context;

        public DeleteModel(library_dbms.Models.InventoryContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AssignedToEmp AssignedToEmp { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AssignedToEmp = await _context.AssignedToEmp
                .Include(a => a.Asset)
                .Include(a => a.Employee).FirstOrDefaultAsync(m => m.AssetId == id);

            if (AssignedToEmp == null)
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

            AssignedToEmp = await _context.AssignedToEmp.FindAsync(id);

            if (AssignedToEmp != null)
            {
                _context.AssignedToEmp.Remove(AssignedToEmp);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
