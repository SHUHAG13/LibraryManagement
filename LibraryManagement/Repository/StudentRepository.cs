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
            try
            {
                return await _dbContext.Students.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw new Exception("An unexpected error occurred while processing the update.", ex);
            }
        }

        public async Task<Student> GetByIdAsync(int id)
         {
            try
            {
                return await _dbContext.Students.FindAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw new Exception("An unexpected error occurred while processing the update.", ex);
            }
        }

        public async  Task<Student> UpdateStudentAsnc(Student student)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw new Exception("An unexpected error occurred while processing the update.", ex);
            }
        }

        public async Task<Student> AddStudentAsync(Student student)
        {
            try
            {
                await _dbContext.Students.AddAsync(student);
                await _dbContext.SaveChangesAsync();
                return student;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw new Exception("An unexpected error occurred while processing the update.", ex);
            }
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            try
            {
                var deleteStudent = await _dbContext.Students.FindAsync(id);
                _dbContext.Students.Remove(deleteStudent);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw new Exception("An unexpected error occurred while processing the update.", ex);
            }
        }

       
    }
}
