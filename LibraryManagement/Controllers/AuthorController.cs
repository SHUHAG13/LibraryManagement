using LibraryManagement.Interfaces;
using LibraryManagement.Models.Domain.Dto;
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
            var authors = await _repository.GetAllAsync();
            return Ok(authors);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _repository.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult>AddAuthor(CreateAuthorDto author)
        { 
            var newAuthor = await _repository.AddAuthorAsync(author);
            return CreatedAtAction(nameof(GetById), new { id = newAuthor.AuthorId }, newAuthor);
            
        }

        [HttpPut]
        public async Task<IActionResult>UpdateAuthor(UpdateAuthorDto authorDto)
        {
           var updatedAuthor = await _repository.UpdateAuthorAsync(authorDto);
           return Ok(updatedAuthor);
        }

        [HttpDelete]
        public async Task<IActionResult>Delete(int id)
        {
            var author = await _repository.DeleteAuthorAsync(id);

            return NoContent();
        }
    }
}
