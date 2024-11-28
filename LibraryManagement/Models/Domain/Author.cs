namespace LibraryManagement.Models.Domain
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public int BooksWritten { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>();

    }
}
