using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using library_dbms.Models;

// TODO:
// - employee assignment update is broken
// - need to handle deletion for assignment to employee/department entities if trying to make the asset unassigned

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
        public AssignedTo AssignedTo { get; set; }
        //[BindProperty]
        //public AssignedToDep AssignedToDep { get; set; }
        //[BindProperty]
        //public AssignedToEmp AssignedToEmp { get; set; }
        [BindProperty]
        public SuppliedBy SuppliedBy { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Asset = await _context.Asset
                .Include(u => u.AssetLocation)
                .Include(u => u.AssignedToEmp)
                .Include(u => u.AssignedToEmp.Employee)
                .Include(u => u.AssignedToDep)
                .Include(u => u.AssignedToDep.DepartmentNumNavigation)
                .FirstOrDefaultAsync(m => m.AssetId == id);

            AssetLocation = await _context.AssetLocation
                .Include(a => a.Asset).FirstOrDefaultAsync(m => m.AssetId == id);

            AssignedTo = new AssignedTo();

            if (_context.AssignedToDep.Any(u => u.AssetId == Asset.AssetId))
            {
                AssignedTo.AssignedToDep = await _context.AssignedToDep
                .Include(a => a.Asset)
                .Include(a => a.DepartmentNumNavigation).FirstOrDefaultAsync(m => m.AssetId == id);
            }

            if (_context.AssignedToEmp.Any(u => u.AssetId == Asset.AssetId)) 
            {
                AssignedTo.AssignedToEmp = await _context.AssignedToEmp
                    .Include(a => a.Asset)
                    .Include(a => a.Employee).FirstOrDefaultAsync(m => m.AssetId == id);
            }

            SuppliedBy = await _context.SuppliedBy.FirstOrDefaultAsync(m => m.AssetId == id);

            if (Asset == null)
            {
                return NotFound();
            }
            if (AssetLocation == null)
            {
                return NotFound();
            }
            if (AssignedTo == null)
            {
                return NotFound();
            }

            ViewData["AssetId"] = new SelectList(_context.Asset, "AssetId", "AssetId");
            ViewData["DepartmentNum"] = new SelectList(_context.Department, "DepartmentNum", "DepartmentName");
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "FirstName");

            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Entry(Asset).State = EntityState.Modified;
            _context.Entry(AssetLocation).State = EntityState.Modified;
            _context.Entry(SuppliedBy).State = EntityState.Modified;

            if (AssignedTo.AssignedToEmp != null){
                _context.Entry(AssignedTo.AssignedToEmp).State = EntityState.Modified;
            }
            else if (AssignedTo.AssignedToDep != null)
            {
                _context.Entry(AssignedTo.AssignedToDep).State = EntityState.Modified;
            }

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
                if (!AssetLocationExists(AssetLocation.AssetId))
                {
                    return NotFound();
                }
                if (!SuppliedByExists(SuppliedBy.AssetId))
                {
                    return NotFound();
                }
                if (!AssignedToDepExists(Asset.AssetId) && !AssignedToEmpExists(Asset.AssetId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AssetExists(int id)
        {
            return _context.Asset.Any(e => e.AssetId == id);
        }
        private bool AssetLocationExists(int id)
        {
            return _context.AssetLocation.Any(e => e.AssetId == id);
        }
        private bool AssignedToDepExists(int id)
        {
            return _context.AssignedToDep.Any(e => e.AssetId == id);
        }
        private bool AssignedToEmpExists(int id)
        {
            return _context.AssignedToEmp.Any(e => e.AssetId == id);
        }
        private bool SuppliedByExists(int id)
        {
            return _context.SuppliedBy.Any(e => e.AssetId == id);
        }
        
        /*
        private bool AssignedToExists(int id)
        {
            if (AssignedTo is AssignedToDep)
                return (_context.AssignedToDep.Any(e => e.AssetId == id));
            else if (AssignedTo is AssignedToEmp)
                return (_context.AssignedToEmp.Any(e => e.AssetId == id));
            else
                return false;
        }*/
    }
}
