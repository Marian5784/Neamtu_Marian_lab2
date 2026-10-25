using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Neamtu_Marian_lab2.Models;

namespace Neamtu_Marian_lab2.Pages.Books;

public class CreateModel : PageModel
{
    private readonly Neamtu_Marian_lab2Context _context;

    public CreateModel(Neamtu_Marian_lab2Context context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        LoadDropDownLists();
        return Page();
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            LoadDropDownLists();
            return Page();
        }

        _context.Book.Add(Book);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }

    private void LoadDropDownLists()
    {
        ViewData["PublisherID"] = new SelectList(
            _context.Set<Publisher>(),
            "ID",
            "PublisherName"
        );

        ViewData["AuthorsID"] = new SelectList(
            _context.Set<Neamtu_Marian_lab2.Models.Authors>()
                .Select(a => new
                {
                    a.ID,
                    FullName = a.FirstName + " " + a.LastName
                }),
            "ID",
            "FullName"
        );
    }
}