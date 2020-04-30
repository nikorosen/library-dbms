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
    public class IndexModel : PageModel
    {
        private readonly library_dbms.Models.InventoryContext _context;

        public IndexModel(library_dbms.Models.InventoryContext context)
        {
            _context = context;
        }

        public IList<Asset> Asset { get;set; }
       
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        
        public SelectList Categories { get; set; }
        [BindProperty(SupportsGet = true)]
        public string AssetCategory { get; set; }

        public SelectList Statuses { get; set; }
        [BindProperty(SupportsGet = true)]
        public string AssetStatus { get; set; }

        public SelectList Manufacturers { get; set; }
        [BindProperty(SupportsGet = true)]
        public string AssetManufacturer { get; set; }


        public async Task OnGetAsync()
        {

            // grab a list of searchable values
            IQueryable<string> categoryQuery = from a in _context.Asset orderby a.Category select a.Category;
            IQueryable<string> statusQuery = from a in _context.Asset orderby a.AssetStatus select a.AssetStatus;
            IQueryable<string> manufacturerQuery = from a in _context.Asset orderby a.Manufacturer select a.Manufacturer;

            // get a list of all assets and 
            // join foreign key values
            var assets = from a in _context.Asset
                .Include(u => u.AssetLocation)
                .Include(u => u.AssignedToDep)
                .Include(u => u.AssignedToEmp) select a;
         
            if (!string.IsNullOrEmpty(SearchString))         {
                assets = assets.Where(x => x.Manufacturer.Contains(SearchString));
            }
         
            if (!string.IsNullOrEmpty(AssetCategory))
            {
                assets = assets.Where(x => x.Category == AssetCategory);
            }

            if (!string.IsNullOrEmpty(AssetStatus))
            {
               assets = assets.Where(x => x.AssetStatus == AssetStatus);
            }
            
            if (!string.IsNullOrEmpty(AssetManufacturer))
            {
                assets = assets.Where(x => x.Manufacturer == AssetManufacturer);
            }

            Categories = new SelectList(await categoryQuery.Distinct().ToListAsync());
            Statuses = new SelectList(await statusQuery.Distinct().ToListAsync());
            Manufacturers = new SelectList(await manufacturerQuery.Distinct().ToListAsync());
            Asset = await assets.ToListAsync();
        }
    }
}
