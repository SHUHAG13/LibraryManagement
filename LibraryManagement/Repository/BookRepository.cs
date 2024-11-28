using LibraryManagement.Data;
using LibraryManagement.Interfaces;
using LibraryManagement.Models.Domain;
using LibraryManagement.Models.Domain.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LibraryManagement.Repository
{
    public class BookRepository : IBookInterface
    {
        private readonly ManagementDbContext _dbContext;

        public BookRepository( ManagementDbContext dbContext)
        {
            _dbContext = dbContext;
            
        }
        public async Task<List<Book>> GetallAsync()
        {
            return await _dbContext.Books.ToListAsync();
           
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _dbContext.Books.FirstOrDefaultAsync(i => i.BookId == id);
        }
        public async Task<Book> AddBookAsync(CreateBookDto book)
        {
            //Mapping CreateBookDto to Book entity
            var bookEntity = new Book
            {
               
                Title = book.Title,
                ISBN = book.ISBN,
                PublishedYear = book.PublishedYear,
                Genre = book.Genre,
                Price = book.Price,
                AuthorId = book.AuthorId

            };


            await _dbContext.Books.AddAsync(bookEntity);
            await _dbContext.SaveChangesAsync();
           

            return bookEntity;
        }

        public Task<Book> UpdateBookAsync(Book book)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var deleteBook = await _dbContext.Books.FirstOrDefaultAsync(i=>i.BookId==id);
            _dbContext.Books.Remove(deleteBook);
            await _dbContext.SaveChangesAsync();
            return true;
        }

       

    }
}
