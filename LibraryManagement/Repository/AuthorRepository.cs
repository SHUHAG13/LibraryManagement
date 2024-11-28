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

       
        public Task<Author> UpdateAuthorAsync(Author book)
        {
            throw new NotImplementedException();
        }
        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}
