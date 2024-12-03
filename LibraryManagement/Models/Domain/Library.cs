using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models.Domain
{
    public class Library
    {
        public int LibraryId { get; set; }
        public string BookName { get; set; }
        public string BookAuthor { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Genre { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}
