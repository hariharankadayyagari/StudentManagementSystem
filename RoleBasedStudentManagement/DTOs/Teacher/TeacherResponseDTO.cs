namespace RoleBasedStudentManagement.DTOs.Teacher
{
    public class TeacherResponseDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name {  get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Department {  get; set; } = string.Empty;
    }
}
