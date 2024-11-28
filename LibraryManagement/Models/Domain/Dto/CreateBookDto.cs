﻿namespace LibraryManagement.Models.Domain.Dto
{
    public class CreateBookDto
    {
       // public int BookId { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int PublishedYear { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public int AuthorId { get; set; }
    }
}