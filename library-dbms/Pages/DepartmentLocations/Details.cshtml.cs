using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using library_dbms.Models;

namespace library_dbms.Pages.DepartmentLocations
{
    public class DetailsModel : PageModel
    {
        private readonly library_dbms.Models.InventoryContext _context;

        public DetailsModel(library_dbms.Models.InventoryContext context)
        {
            _context = context;
        }

        public DepartmentLocation DepartmentLocation { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DepartmentLocation = await _context.DepartmentLocation
                .Include(d => d.DepartmentNumNavigation).FirstOrDefaultAsync(m => m.DepartmentNum == id);

            if (DepartmentLocation == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
