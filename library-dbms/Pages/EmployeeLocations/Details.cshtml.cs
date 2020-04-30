using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using library_dbms.Models;

namespace library_dbms.Pages.EmployeeLocations
{
    public class DetailsModel : PageModel
    {
        private readonly library_dbms.Models.InventoryContext _context;

        public DetailsModel(library_dbms.Models.InventoryContext context)
        {
            _context = context;
        }

        public EmployeeLocation EmployeeLocation { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EmployeeLocation = await _context.EmployeeLocation
                .Include(e => e.Employee).FirstOrDefaultAsync(m => m.EmployeeId == id);

            if (EmployeeLocation == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
