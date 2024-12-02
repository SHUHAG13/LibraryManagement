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
            var books = await _repository.GetallAsync();
            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult>GetbyId(int id)
        {
            var book = await _repository.GetByIdAsync(id);
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult>AddBook(CreateBookDto book)
        {
            var newBook = await _repository.AddBookAsync(book);
            return CreatedAtAction(nameof(GetbyId), 
                new { id = newBook.BookId }, newBook);
        }

        [HttpPut]
        public async Task<IActionResult>UpadateBook(UpdateBookDto updateDto )
        {
            var updateBook = await _repository.UpdateBookAsync(updateDto);
            return Ok(updateDto);
        }

        [HttpDelete]
        public async Task<IActionResult>Delete(int id)
        {
            var isDeleted = await _repository.DeleteBookAsync(id);
            return NoContent();
        }
    }
}
