namespace LibraryManagement.Models.Domain.Dto
{
    public class CreateAuthorDto
    {
       // public int AuthorId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public int BooksWritten { get; set; }
    }
}
