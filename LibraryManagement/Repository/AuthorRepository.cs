using LibraryManagement.Data;
using LibraryManagement.Interfaces;
using LibraryManagement.Models.Domain;
using LibraryManagement.Models.Domain.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repository
{
    public class AuthorRepository : IAuthorInterface
    {
        private readonly ManagementDbContext _dbContext;

        public AuthorRepository(ManagementDbContext context)
        {
            _dbContext = context;
        }
        public async Task<List<Response<Author>>> GetAllAsync()
        {
            try
            {
                var authors = await _dbContext.Authors.Include(a => a.Books).ToListAsync();
                var responseList = authors.Select(author => new Response<Author>
                {
                    StatusCode = 200,
                    Message = "Success",
                    Data = author
                }).ToList();

                return responseList;
            }
            catch (Exception ex)
            {
            return new List<Response<Author>>
              {
               new Response<Author>
              {
                StatusCode = 500,
                Message = $"Internal server error: {ex.Message}",
              }
              };
            }
        }


        public async Task<Response<Author>> GetByIdAsync(int id)
        {
            try
            {
                var result = await _dbContext.Authors
                    .Include(i => i.Books)
                    .FirstOrDefaultAsync(i => i.AuthorId == id);

                if(result == null)
                {
                    return new Response<Author>()
                    {
                        StatusCode = 404,
                        Message = $"Author with id {id} not found"
                    };
                }

                return new Response<Author>()
                {
                    StatusCode = 200,
                    Message = "Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new Response<Author>()
                {
                    StatusCode = 500,
                    Message = $"Internal server error :{ex.Message}"
                };
            }
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
            try
            {
                var newAuthor = await _dbContext.Authors.AddAsync(authorDto);
                await _dbContext.SaveChangesAsync();
                return authorDto;
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal server error: {ex.Message}", ex); 
            }



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
