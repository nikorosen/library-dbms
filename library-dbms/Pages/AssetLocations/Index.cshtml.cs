using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using library_dbms.Models;
using System.Security.Cryptography.X509Certificates;

namespace library_dbms.Pages.AssetLocations
{
    public class IndexModel : PageModel
    {
        private readonly library_dbms.Models.InventoryContext _context;

        public IndexModel(library_dbms.Models.InventoryContext context)
        {
            _context = context;
        }

        public IList<AssetLocation> AssetLocation { get;set; }
        
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task OnGetAsync()
        {
            var assetLocations = from a in _context.AssetLocation.Include(u => u.Asset) select a;

            if (!string.IsNullOrEmpty(SearchString))
            {
                assetLocations = assetLocations.Where(x => x.AssetId == Int32.Parse(SearchString));
            }

            AssetLocation = await assetLocations.ToListAsync();
        }
    }
}
