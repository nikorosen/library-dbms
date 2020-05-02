using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using library_dbms.Models;

namespace library_dbms.Pages.Assets
{
    public class EditModel : PageModel
    {
        private readonly library_dbms.Models.InventoryContext _context;

        public EditModel(library_dbms.Models.InventoryContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Asset Asset { get; set; }
        [BindProperty]
        public AssetLocation AssetLocation { get; set; }
        [BindProperty]
        public AssignedToDep AssignedToDep { get; set; }
        [BindProperty]
        public AssignedToEmp AssignedToEmp { get; set; }
        [BindProperty]
        public SuppliedBy SuppliedBy { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["DepartmentNum"] = new SelectList(_context.Department, "DepartmentNum", "DepartmentName");
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "FirstName");

            Asset = await _context.Asset
                .Include(u => u.AssetLocation)
                .Include(u => u.AssignedToEmp)
                .Include(u => u.AssignedToEmp.Employee)
                .Include(u => u.AssignedToDep)
                .Include(u => u.AssignedToDep.DepartmentNumNavigation)
                .FirstOrDefaultAsync(m => m.AssetId == id);

            if (Asset == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            int assetId = Asset.AssetId;

            SuppliedBy.AssetId = assetId;
            AssetLocation.AssetId = assetId;
            AssignedToEmp.AssetId = assetId;
            AssignedToDep.AssetId = assetId;

            _context.Attach(Asset).State = EntityState.Modified;
            

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetExists(Asset.AssetId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            ///
            // Asset editing is a WIP. Throws error when trying to update foreign key values on the same page
            // as asset editing. I believe this is because the EntityState can't be modified before it's saved 
            // and/or posted. For now, I've kept the editing fields in the cshtml, 
            // but they'll only work for asset attributes
            ///

            /*
            _context.Attach(AssetLocation).State = EntityState.Modified;
            _context.Attach(SuppliedBy).State = EntityState.Modified;
            _context.Attach(AssignedToDep).State = EntityState.Modified;
            _context.Attach(AssignedToEmp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetExists(Asset.AssetId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            */

            return RedirectToPage("./Index");
        }

        private bool AssetExists(int id)
        {
            return _context.Asset.Any(e => e.AssetId == id);
        }
    }
}
