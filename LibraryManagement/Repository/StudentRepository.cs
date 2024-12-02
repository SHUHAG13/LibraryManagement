using LibraryManagement.Data;
using LibraryManagement.Interfaces;
using LibraryManagement.Models.Domain;
using Microsoft.EntityFrameworkCore;


namespace LibraryManagement.Repository
{
    public class StudentRepository : IStudentInterface
    {
        private readonly ManagementDbContext _dbContext;

        public StudentRepository(ManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Student>> GetAllAsync()
        {
            return await _dbContext.Students.ToListAsync();
        }

        public async Task<Student> GetByIdAsync(int id)
         {
             return await _dbContext.Students.FindAsync(id);
         }

        public async  Task<Student> UpdateStudentAsnc(Student student)
        {
            var existingStudent = await _dbContext.Students.FindAsync(student.StudentId);
            existingStudent.Name = student.Name;
            existingStudent.Email = student.Email;
            existingStudent.PhoneNumber = student.PhoneNumber;
            existingStudent.Address = student.Address;
            existingStudent.EnrollmentDate = student.EnrollmentDate;
            existingStudent.StudentCardNo = student.StudentCardNo;
            existingStudent.IsActive = student.IsActive;
            await _dbContext.SaveChangesAsync();
            return student;    
        }

        public async Task<Student> AddStudentAsync(Student student)
        {
            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();
            return student;
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var deleteStudent = await _dbContext.Students.FindAsync(id);
            _dbContext.Students.Remove(deleteStudent);
            await _dbContext.SaveChangesAsync();
            return true;
        }

       
    }
}
