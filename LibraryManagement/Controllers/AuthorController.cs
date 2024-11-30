using LibraryManagement.Interfaces;
using LibraryManagement.Models.Domain;
using LibraryManagement.Models.Domain.Dto;
using LibraryManagement.Repository;
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
                var authors= await _repository.GetallAsync();
                if(authors == null)
                {
                    return NoContent();
                }
               
                return Ok(authors);

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
                var author = await _repository.GetByIdAsync(id);
                if (author == null)
                {
                    return NotFound($"Author with id {id} not found");
                }
                return Ok(author);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error :{ex.Message}");
            }

        }
        [HttpPost]
        public async Task<IActionResult>AddAuthor(CreateAuthorDto author)
        {
            if (author == null)
            {
                return BadRequest("Author data cannot be null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input data.");
            }
            try
            {
                var newAuthor = await _repository.AddAuthorAsync(author);
                return CreatedAtAction(nameof(GetById), new { id = newAuthor.AuthorId }, newAuthor);
                return Ok(author);
            }
          
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error :{ex.Message}");
            }

        }
        [HttpPut]
        public async Task<IActionResult>UpdateAuthor(UpdateAuthorDto authorDto)
        {
            if (authorDto == null)
            {
                return BadRequest("Author data is required.");
            }
            try
            {
                var updatedAuthor = await _repository.UpdateAuthorAsync(authorDto);
                if (updatedAuthor == null)
                {

                    return NotFound($"Author with ID {authorDto.AuthorId} not found.");
                }
                return Ok(updatedAuthor);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error :{ex.Message}");
            }

        }
        [HttpDelete]
        public async Task<IActionResult>Delete(int id)
        {
            try
            {
                var author = await _repository.DeleteAuthorAsync(id);
                if(author==null)
                {
                  return NotFound($"Author with id {id}  not found");
                }
                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error :{ex.Message}");
            }


        }
    }
}
