using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using library_dbms.Models;
using Microsoft.EntityFrameworkCore;

namespace library_dbms.Pages.Assets
{
    public class CreateModel : PageModel
    {
        private readonly library_dbms.Models.InventoryContext _context;

        public SelectList Categories { get; set; }
        public SelectList Statuses { get; set; }
        public SelectList Manufacturers { get; set; }


        public CreateModel(library_dbms.Models.InventoryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            IQueryable<string> categoryQuery = from a in _context.Asset orderby a.Category select a.Category;
            IQueryable<string> statusQuery = from a in _context.Asset orderby a.AssetStatus select a.AssetStatus;
            IQueryable<string> manufacturerQuery = from a in _context.Asset orderby a.Manufacturer select a.Manufacturer;
            Categories = new SelectList(categoryQuery.Distinct().ToList());
            Statuses = new SelectList(statusQuery.Distinct().ToList());
            Manufacturers = new SelectList(manufacturerQuery.Distinct().ToList());

            return Page();
        }

        [BindProperty]
        public Asset Asset { get; set; }
        //[BindProperty]
        //public AssetLocation AssetLocation { get; set; }
        

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            

            if (!ModelState.IsValid)
            {
                return Page();
            }

            //int AssetId = Asset.AssetId;

            _context.Asset.Add(Asset);
            await _context.SaveChangesAsync();

            //_context.AssetLocation.Add(AssetLocation);
            //await _context.SaveChangesAsync();

            return RedirectToPage("./_Create", new { id = Asset.AssetId });
            //return RedirectToPage("../AssetLocations/Create", new { id = Asset.AssetId });
        }
    }
}
