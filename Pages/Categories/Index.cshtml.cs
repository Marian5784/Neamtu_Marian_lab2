using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Neamtu_Marian_lab2.Models;

namespace Neamtu_Marian_lab2.Pages.Categories;

public class IndexModel : PageModel
{
    private readonly Neamtu_Marian_lab2Context _context;

    public IndexModel(Neamtu_Marian_lab2Context context)
    {
        _context = context;
    }

    public IList<Category> Category { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Category = await _context.Category.ToListAsync();
    }
}
