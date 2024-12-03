using LibraryManagement.Interfaces;
using LibraryManagement.Models.Domain;
using LibraryManagement.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryInterface _libraryRepository;

        public LibraryController(ILibraryInterface libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var libraries = await _libraryRepository.GetAllAsync();
            return Ok(libraries);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var library = await _libraryRepository.GetByIdAsync(id);
            if (library == null) return NotFound("Library not found");
            return Ok(library);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Library library)
        {
            var newLibrary = await _libraryRepository.AddAsync(library);
            return CreatedAtAction(nameof(GetById), new { id = newLibrary.LibraryId }, newLibrary);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Library library)
        {
            var updatedLibrary = await _libraryRepository.UpdateAsync(library);
            if (updatedLibrary == null) return NotFound("Library not found");
            return Ok(updatedLibrary);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _libraryRepository.DeleteAsync(id);
            if (!result) return NotFound("Library not found");
            return NoContent();
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchBooks([FromQuery] string bookName, [FromQuery] string bookAuthor)
        {
            var books = await _libraryRepository.SearchBooksAsync(bookName, bookAuthor);

            if (books == null || books.Count == 0)
            {
                return NotFound("No books found matching the search criteria.");
            }

            return Ok(books);
        }
    }
}
