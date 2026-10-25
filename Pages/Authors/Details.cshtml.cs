using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Neamtu_Marian_lab2.Models;

namespace Neamtu_Marian_lab2.Pages.Authors
{
    public class DetailsModel : PageModel
    {
        private readonly Neamtu_Marian_lab2Context _context;

        public DetailsModel(Neamtu_Marian_lab2Context context)
        {
            _context = context;
        }

        public Neamtu_Marian_lab2.Models.Authors Authors { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authors = await _context.Authors
                .FirstOrDefaultAsync(m => m.ID == id);

            if (authors == null)
            {
                return NotFound();
            }

            Authors = authors;

            return Page();
        }
    }
}