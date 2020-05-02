using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using library_dbms.Models;

namespace library_dbms.Pages.AssetLocations
{
    public class CreateModel : PageModel
    {
        private readonly library_dbms.Models.InventoryContext _context;

        public CreateModel(library_dbms.Models.InventoryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["AssetId"] = new SelectList(_context.Asset, "AssetId", "AssetId");
            ViewData["DepartmentNum"] = new SelectList(_context.Department, "DepartmentNum", "DepartmentName");
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "FirstName");
            return Page();
        }

        [BindProperty]
        public AssetLocation AssetLocation { get; set; }
        [BindProperty]
        public AssignedToDep AssignedToDep { get; set; }
        [BindProperty]
        public AssignedToEmp AssignedToEmp { get; set; }
        [BindProperty]
        public SuppliedBy SuppliedBy { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)//int? AssetId)
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            int assetId = (int)id;
            string assignTo = Request.Form["assign-to"][0];

            SuppliedBy.AssetId = assetId;
            AssetLocation.AssetId = assetId;
            _context.SuppliedBy.Add(SuppliedBy);
            _context.AssetLocation.Add(AssetLocation);
            
            if (assignTo == "department")
            {
                AssignedToDep.AssetId = assetId;
                _context.AssignedToDep.Add(AssignedToDep);
            }
            else if (assignTo == "employee")
            {
                AssignedToEmp.AssetId = assetId;
                _context.AssignedToEmp.Add(AssignedToEmp);
            }
            
            await _context.SaveChangesAsync();

            return RedirectToPage("../Assets/Index");
        }
    }
}
