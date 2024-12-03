using LibraryManagement.Interfaces;
using LibraryManagement.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentInterface _repository;

        public StudentController(IStudentInterface repository)
        {
            _repository = repository;    
        }

        [HttpGet]
        public async Task<IActionResult>GetAll()
        {
            var students = await _repository.GetAllAsync();
            return Ok(students);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult>GetById(int id)
        {
            var student = await _repository.GetByIdAsync(id);
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult>CreateStudent(Student student)
        {
            var newStudent = await _repository.AddStudentAsync(student);
            return Ok(newStudent);
        }

        [HttpPut]
        public async Task<IActionResult>UpdateStudent(Student student)
        {
            var updateStudent = await _repository.UpdateStudentAsnc(student);
            return Ok(updateStudent);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult>DeleteStudent(int id)
        {
            await _repository.DeleteStudentAsync(id);
            return NoContent();
        }
        }
}
