using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Neamtu_Marian_lab2.Models;

namespace Neamtu_Marian_lab2.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly Neamtu_Marian_lab2Context _context;

        public IndexModel(Neamtu_Marian_lab2Context context)
        {
            _context = context;
        }

        public IList<Book> Book { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Book = await _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.Authors)
                .ToListAsync();
        }
    }
}