using LibraryManagement.Data;
using LibraryManagement.Interfaces;
using LibraryManagement.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repository
{
    public class LibraryRepository:ILibraryInterface
    {
        private readonly ManagementDbContext _context;

        public LibraryRepository(ManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<Library>> SearchBooksAsync(string bookName, string bookAuthor)
        {
            var query = _context.Libraries.AsQueryable();

            if (!string.IsNullOrEmpty(bookName))
            {
                query = query.Where(b => EF.Functions.Like(b.BookName, $"%{bookName}%"));
            }

            if (!string.IsNullOrEmpty(bookAuthor))
            {
                query = query.Where(b => EF.Functions.Like(b.BookAuthor, $"%{bookAuthor}%"));
            }

            return await query.ToListAsync();
        }

        public async Task<List<Library>> GetAllAsync()
        {
            try
            {
                return await _context.Libraries.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving all libraries: {ex.Message}", ex);
            }
        }

        public async Task<Library> GetByIdAsync(int libraryId)
        {
            try
            {
                var library = await _context.Libraries.FindAsync(libraryId);
                if (library == null) throw new KeyNotFoundException("Library not found");
                return library;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving library by ID: {ex.Message}", ex);
            }
        }

        public async Task<Library> AddAsync(Library library)
        {
            try
            {
                await _context.Libraries.AddAsync(library);
                await _context.SaveChangesAsync();
                return library;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding new library: {ex.Message}", ex);
            }
        }

        public async Task<Library> UpdateAsync(Library library)
        {
            try
            {
                var existingLibrary = await _context.Libraries.FindAsync(library.LibraryId);
                if (existingLibrary == null) throw new KeyNotFoundException("Library not found");

                existingLibrary.BookName = library.BookName;
                existingLibrary.BookAuthor = library.BookAuthor;
                existingLibrary.PublishedDate = library.PublishedDate;
                existingLibrary.Genre = library.Genre;
                existingLibrary.IsAvailable = library.IsAvailable;

                await _context.SaveChangesAsync();
                return existingLibrary;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating library: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteAsync(int libraryId)
        {
            try
            {
                var library = await _context.Libraries.FindAsync(libraryId);
                if (library == null) throw new KeyNotFoundException("Library not found");

                _context.Libraries.Remove(library);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting library: {ex.Message}", ex);
            }
        }
    }
}
