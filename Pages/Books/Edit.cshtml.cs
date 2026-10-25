using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Neamtu_Marian_lab2.Models;

namespace Neamtu_Marian_lab2.Pages.Books
{
    public class EditModel : BookCategoriesPageModel
    {
        private readonly Neamtu_Marian_lab2Context _context;

        public EditModel(Neamtu_Marian_lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.Authors)
                .Include(b => b.BookCategories)
                    .ThenInclude(bc => bc.Category)
                .FirstOrDefaultAsync(b => b.ID == id);

            if (book == null)
            {
                return NotFound();
            }

            Book = book;

            PopulateAssignedCategoryData(_context, Book);
            LoadDropDownLists();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(
            int? id,
            string[] selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookToUpdate = await _context.Book
                .Include(b => b.BookCategories)
                .FirstOrDefaultAsync(b => b.ID == id);

            if (bookToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Book>(
                bookToUpdate,
                "Book",
                b => b.Title,
                b => b.Price,
                b => b.PublishingDate,
                b => b.PublisherID,
                b => b.AuthorsID))
            {
                UpdateBookCategories(
                    _context,
                    selectedCategories,
                    bookToUpdate);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(bookToUpdate.ID))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToPage("./Index");
            }

            Book = bookToUpdate;

            PopulateAssignedCategoryData(_context, Book);
            LoadDropDownLists();

            return Page();
        }

        private void LoadDropDownLists()
        {
            ViewData["PublisherID"] = new SelectList(
                _context.Set<Publisher>(),
                "ID",
                "PublisherName",
                Book.PublisherID
            );

            ViewData["AuthorsID"] = new SelectList(
                _context.Set<Neamtu_Marian_lab2.Models.Authors>()
                    .Select(a => new
                    {
                        a.ID,
                        FullName = a.FirstName + " " + a.LastName
                    }),
                "ID",
                "FullName",
                Book.AuthorsID
            );
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.ID == id);
        }
    }
}