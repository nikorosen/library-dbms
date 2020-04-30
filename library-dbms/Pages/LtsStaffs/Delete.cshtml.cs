using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using library_dbms.Models;

namespace library_dbms.Pages.LtsStaffs
{
    public class DeleteModel : PageModel
    {
        private readonly library_dbms.Models.InventoryContext _context;

        public DeleteModel(library_dbms.Models.InventoryContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LtsStaff LtsStaff { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LtsStaff = await _context.LtsStaff
                .Include(l => l.Employee).FirstOrDefaultAsync(m => m.EmployeeId == id);

            if (LtsStaff == null)
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

            LtsStaff = await _context.LtsStaff.FindAsync(id);

            if (LtsStaff != null)
            {
                _context.LtsStaff.Remove(LtsStaff);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
