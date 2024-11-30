using LibraryManagement.Models.Domain;
using LibraryManagement.Models.Domain.Dto;

namespace LibraryManagement.Interfaces
{
    public interface IBookInterface
    {
        Task<List<Book>> GetallAsync();
        Task<Book> GetByIdAsync(int id);
        Task<Book> AddBookAsync(CreateBookDto book);
        Task<Book> UpdateBookAsync(UpdateBookDto updateDto);
        Task<Book> DeleteBookAsync(int id);
    }
}
