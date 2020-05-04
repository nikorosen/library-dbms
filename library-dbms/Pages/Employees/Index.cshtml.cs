using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using library_dbms.Models;

namespace library_dbms.Pages.Employees
{
    public class IndexModel : PageModel
    {
        private readonly library_dbms.Models.InventoryContext _context;

        public IndexModel(library_dbms.Models.InventoryContext context)
        {
            _context = context;
        }

        public IList<Employee> Employee { get;set; }
        
        [BindProperty(SupportsGet = true)]
        public string SearchFirstName{ get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchLastName { get; set; }

        public async Task OnGetAsync()
        {

            var employees = from a in _context.Employee select a;

            if (!string.IsNullOrEmpty(SearchFirstName))
            {
                employees = employees.Where(x => x.FirstName.Contains(SearchFirstName));
            }
            if (!string.IsNullOrEmpty(SearchLastName))
            {
                employees = employees.Where(x => x.LastName.Contains(SearchLastName));
            }

            Employee = await employees.ToListAsync();
        }
    }
}
