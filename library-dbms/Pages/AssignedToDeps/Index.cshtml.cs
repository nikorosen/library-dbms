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
    public class IndexModel : PageModel
    {
        private readonly library_dbms.Models.InventoryContext _context;

        public IndexModel(library_dbms.Models.InventoryContext context)
        {
            _context = context;
        }

        public IList<AssignedToDep> AssignedToDep { get;set; }

        public async Task OnGetAsync()
        {
            AssignedToDep = await _context.AssignedToDep
                .Include(a => a.Asset)
                .Include(a => a.DepartmentNumNavigation).ToListAsync();
        }
    }
}
