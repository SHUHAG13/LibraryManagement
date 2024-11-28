using LibraryManagement.Interfaces;
using LibraryManagement.Models.Domain;
using LibraryManagement.Models.Domain.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookInterface _repository;

        public BookController(IBookInterface repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var books = await _repository.GetallAsync();
                if (books == null)
                {
                    return NotFound("Books not Found");
                }
                return Ok(books);

            }catch(Exception ex){
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult>GetbyId(int id)
        {
            try
            {
                var book = await _repository.GetByIdAsync(id);
                if (book == null)
                {
                    return NotFound($"Book with id {id} not found");
                }
                return Ok(book);

            }catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error :{ex.Message}");
            }
            
        }
        [HttpPost]
        public async Task<IActionResult>AddBook(CreateBookDto book)
        {
            if (book == null)
            {
                return BadRequest("Book cannot be null.");
            }
            try
            {
                var newBook = await _repository.AddBookAsync(book);
                return CreatedAtAction(nameof(GetbyId), new { id = newBook.BookId }, newBook);
            }catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}. Inner Exception: {ex.InnerException?.Message}");
            }
        }
        [HttpDelete]
        public async Task<IActionResult>Delete(int id)
        {
            var book = await _repository.DeleteBookAsync(id);
            return Ok($"Book delete with id {id} is successfull");
        }
    }
}
