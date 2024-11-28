using LibraryManagement.Models.Domain;
using LibraryManagement.Models.Domain.Dto;

namespace LibraryManagement.Interfaces
{
    public interface IBookInterface
    {
        Task<List<Book>> GetallAsync();
        Task<Book> GetByIdAsync(int id);
        Task<Book> AddBookAsync(CreateBookDto book);
        Task<Book> UpdateBookAsync(Book book);
        Task<bool> Delete(int id);
    }
}
