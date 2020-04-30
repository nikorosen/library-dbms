using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using library_dbms.Models;

namespace library_dbms.Pages.AssignedToDeps
{
    public class DetailsModel : PageModel
    {
        private readonly library_dbms.Models.InventoryContext _context;

        public DetailsModel(library_dbms.Models.InventoryContext context)
        {
            _context = context;
        }

        public AssignedToDep AssignedToDep { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AssignedToDep = await _context.AssignedToDep
                .Include(a => a.Asset)
                .Include(a => a.DepartmentNumNavigation).FirstOrDefaultAsync(m => m.AssetId == id);

            if (AssignedToDep == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
