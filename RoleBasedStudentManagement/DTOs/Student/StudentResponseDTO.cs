
namespace RoleBasedStudentManagement.DTOs.Student

{
    public class StudentResponseDTO
    {
        public int Id {  get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Course { get; set; } = string.Empty;
        public int Year {  get; set; }

    }
}
