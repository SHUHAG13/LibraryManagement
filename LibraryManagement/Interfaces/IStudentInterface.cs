using LibraryManagement.Models.Domain;

namespace LibraryManagement.Interfaces
{
    public interface IStudentInterface
    {
        Task<List<Student>> GetAllAsync();
        Task<Student> GetByIdAsync(int id);
        Task<Student> AddStudentAsync(Student student);
        Task<Student> UpdateStudentAsnc(Student student);
        Task<bool> DeleteStudentAsync(int id);
    }
}
