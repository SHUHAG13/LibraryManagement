using LibraryManagement.Data;
using LibraryManagement.Interfaces;
using LibraryManagement.Models.Domain;
using LibraryManagement.Models.Domain.Dto;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace LibraryManagement.Repository
{
    public class AuthorRepository : IAuthorInterface
    {
        private readonly ManagementDbContext _dbContext;

        public AuthorRepository(ManagementDbContext context)
        {
            _dbContext = context;
        }
        public async Task<List<Author>> GetallAsync()
        {
            var authors = await _dbContext.Authors.Include(i => i.Books).ToListAsync();

            return authors;

        }

        public async Task<Author> GetByIdAsync(int id)
        {
            return await _dbContext.Authors.FirstOrDefaultAsync(i => i.AuthorId == id);
        }
        public async Task<Author> AddAuthorAsync(CreateAuthorDto author)
        {
            var authorDto = new Author
            {
                //AuthorId=author.AuthorId,
                Name=author.Name,
                DateOfBirth=author.DateOfBirth,
                Nationality=author.Nationality,
                BooksWritten=author.BooksWritten

            };

            var newAuthor = await _dbContext.Authors.AddAsync(authorDto);
            await _dbContext.SaveChangesAsync();
            return authorDto;
        }

       
        public async Task<Author> UpdateAuthorAsync(UpdateAuthorDto authorDto)
        {
            var existingAuthor = await _dbContext.Authors.FindAsync(authorDto.AuthorId);
            if(existingAuthor==null)
            {
                return null;
            }
            existingAuthor.Name = authorDto.Name;
            existingAuthor.DateOfBirth = authorDto.DateOfBirth;
            existingAuthor.Nationality = authorDto.Nationality;
            existingAuthor.BooksWritten = authorDto.BooksWritten;

            await _dbContext.SaveChangesAsync();
            return existingAuthor;
           
        }
        public async Task<Author> DeleteAuthorAsync(int id)
        {
            var todeleteAuthor = await _dbContext.Authors.FirstOrDefaultAsync(i => i.AuthorId == id);
            
            _dbContext.Authors.Remove(todeleteAuthor);
            await _dbContext.SaveChangesAsync();
            return todeleteAuthor;
        }

    }
}
