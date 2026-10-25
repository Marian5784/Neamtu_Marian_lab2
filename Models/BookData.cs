namespace Neamtu_Marian_lab2.Models
{
    public class BookData
    {
        public IEnumerable<Book> Books { get; set; }
            = new List<Book>();

        public IEnumerable<Category> Categories { get; set; }
            = new List<Category>();

        public IEnumerable<BookCategory> BookCategories { get; set; }
            = new List<BookCategory>();
    }
}