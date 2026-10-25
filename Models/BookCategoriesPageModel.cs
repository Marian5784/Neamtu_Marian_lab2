using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Neamtu_Marian_lab2.Models
{
    public class BookCategoriesPageModel : PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList { get; set; }
            = new List<AssignedCategoryData>();

        public void PopulateAssignedCategoryData(
            Neamtu_Marian_lab2Context context,
            Book book)
        {
            var allCategories = context.Category;

            var bookCategories = new HashSet<int>(
                book.BookCategories?
                    .Select(c => c.CategoryID)
                ?? Enumerable.Empty<int>());

            AssignedCategoryDataList = new List<AssignedCategoryData>();

            foreach (var category in allCategories)
            {
                AssignedCategoryDataList.Add(new AssignedCategoryData
                {
                    CategoryID = category.ID,
                    Name = category.CategoryName,
                    Assigned = bookCategories.Contains(category.ID)
                });
            }
        }

        public void UpdateBookCategories(
            Neamtu_Marian_lab2Context context,
            string[] selectedCategories,
            Book bookToUpdate)
        {
            if (selectedCategories == null)
            {
                selectedCategories = Array.Empty<string>();
            }

            var selectedCategoriesHS = new HashSet<string>(
                selectedCategories);

            var bookCategories = new HashSet<int>(
                bookToUpdate.BookCategories?
                    .Select(c => c.CategoryID)
                ?? Enumerable.Empty<int>());

            foreach (var category in context.Category)
            {
                if (selectedCategoriesHS.Contains(category.ID.ToString()))
                {
                    if (!bookCategories.Contains(category.ID))
                    {
                        bookToUpdate.BookCategories ??=
                            new List<BookCategory>();

                        bookToUpdate.BookCategories.Add(new BookCategory
                        {
                            BookID = bookToUpdate.ID,
                            CategoryID = category.ID
                        });
                    }
                }
                else
                {
                    if (bookCategories.Contains(category.ID))
                    {
                        var categoryToRemove =
                            bookToUpdate.BookCategories!
                                .FirstOrDefault(
                                    c => c.CategoryID == category.ID);

                        if (categoryToRemove != null)
                        {
                            context.BookCategory.Remove(categoryToRemove);
                        }
                    }
                }
            }
        }
    }
}