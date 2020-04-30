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
    public class DetailsModel : PageModel
    {
        private readonly library_dbms.Models.InventoryContext _context;

        public DetailsModel(library_dbms.Models.InventoryContext context)
        {
            _context = context;
        }

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
    }
}
