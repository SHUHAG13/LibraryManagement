using LibraryManagement.Interfaces;
using LibraryManagement.Models.Domain;
using LibraryManagement.Models.Domain.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorInterface _repository;

        public AuthorController(IAuthorInterface repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var books= await _repository.GetallAsync();
               
                return Ok(books);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error :{ex.Message}");
            }

        }
        [HttpGet("{id:int}")]

        public async Task<IActionResult>GetById(int id)
        {
            try
            {
                var book = await _repository.GetByIdAsync(id);
                if (book == null)
                {
                    return NotFound($"Author with id {id} not found");
                }
                return Ok(book);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error :{ex.Message}");
            }

        }
        [HttpPost]
        public async Task<IActionResult>AddAuthor(CreateAuthorDto author)
        {
            var newAuthor = await _repository.AddAuthorAsync(author);
            if (newAuthor == null)
            {
                return BadRequest("Book cannot be null.");
            }
            return CreatedAtAction(nameof(GetById), new { id = newAuthor.AuthorId }, newAuthor);
            return Ok(author);
        }
    }
}
