﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using library_dbms.Models;

namespace library_dbms.Pages.PerformedBys
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
        ViewData["LogId"] = new SelectList(_context.MaintenanceLog, "LogId", "Description");
            return Page();
        }

        [BindProperty]
        public PerformedBy PerformedBy { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.PerformedBy.Add(PerformedBy);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
