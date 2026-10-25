using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Neamtu_Marian_lab2.Models;

namespace Neamtu_Marian_lab2.Pages.Books
{
    public class CreateModel : BookCategoriesPageModel
    {
        private readonly Neamtu_Marian_lab2Context _context;

        public CreateModel(Neamtu_Marian_lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public IActionResult OnGet()
        {
            Book = new Book();

            PopulateAssignedCategoryData(_context, Book);
            LoadDropDownLists();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            if (!ModelState.IsValid)
            {
                PopulateAssignedCategoryData(_context, Book);
                MarkSelectedCategories(selectedCategories);
                LoadDropDownLists();

                return Page();
            }

            Book.BookCategories = new List<BookCategory>();

            foreach (var categoryID in selectedCategories)
            {
                if (int.TryParse(categoryID, out int id))
                {
                    Book.BookCategories.Add(new BookCategory
                    {
                        CategoryID = id
                    });
                }
            }

            _context.Book.Add(Book);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private void MarkSelectedCategories(string[] selectedCategories)
        {
            foreach (var category in AssignedCategoryDataList)
            {
                category.Assigned =
                    selectedCategories.Contains(category.CategoryID.ToString());
            }
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
    }
}