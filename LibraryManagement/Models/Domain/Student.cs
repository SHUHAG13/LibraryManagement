using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models.Domain
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; } 
        public string Email { get; set; } 
        public string PhoneNumber { get; set; } 
        public string Address { get; set; } 
        public DateTime EnrollmentDate { get; set; } 
        public string StudentCardNo { get; set; } 
        public bool IsActive { get; set; } = true; 

        
       
    }
}
