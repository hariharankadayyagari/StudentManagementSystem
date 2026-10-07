namespace RoleBasedStudentManagement.DTOs.Student
{
    public class UpdateStudentDTO
    {
        public string Name {  get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Department {  get; set; } = string.Empty;
        public string Course {  get; set; } = string.Empty;
        public int Year {  get; set; }
    }
}
