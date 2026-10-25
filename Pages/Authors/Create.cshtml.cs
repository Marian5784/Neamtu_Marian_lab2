using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Neamtu_Marian_lab2.Models;

namespace Neamtu_Marian_lab2.Pages.Authors
{
    public class CreateModel : PageModel
    {
        private readonly Neamtu_Marian_lab2Context _context;

        public CreateModel(Neamtu_Marian_lab2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Neamtu_Marian_lab2.Models.Authors Authors { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Authors.Add(Authors);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}