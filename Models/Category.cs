namespace Neamtu_Marian_lab2.Models
{
    public class Category
    {
        public int ID { get; set; }

        public string CategoryName { get; set; }

        public ICollection<BookCategory>? BookCategories { get; set; }
    }
}