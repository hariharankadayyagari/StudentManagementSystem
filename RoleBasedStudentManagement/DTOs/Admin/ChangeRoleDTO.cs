namespace RoleBasedStudentManagement.DTOs.Admin
{
    public class ChangeRoleDTO
    {
        public string Role { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Course { get; set; } = string.Empty;
        public int Year { get; set; }
    }
}
