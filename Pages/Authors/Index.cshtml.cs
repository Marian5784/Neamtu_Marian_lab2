using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Neamtu_Marian_lab2.Models;

namespace Neamtu_Marian_lab2.Pages.Authors
{
    public class IndexModel : PageModel
    {
        private readonly Neamtu_Marian_lab2Context _context;

        public IndexModel(Neamtu_Marian_lab2Context context)
        {
            _context = context;
        }

        public IList<Neamtu_Marian_lab2.Models.Authors> Authors { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Authors = await _context.Authors.ToListAsync();
        }
    }
}