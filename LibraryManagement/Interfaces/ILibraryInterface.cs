using LibraryManagement.Models.Domain;

namespace LibraryManagement.Interfaces
{
    public interface ILibraryInterface
    {
        Task<List<Library>> SearchBooksAsync(string bookName, string bookAuthor);
        Task<List<Library>> GetAllAsync();
        Task<Library> GetByIdAsync(int libraryId);
        Task<Library> AddAsync(Library library);
        Task<Library> UpdateAsync(Library library);
        Task<bool> DeleteAsync(int libraryId);
    }
}
