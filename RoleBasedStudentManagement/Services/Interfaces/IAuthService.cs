using RoleBasedStudentManagement.DTOs.Auth;



namespace RoleBasedStudentManagement.Services.Interfaces
{
    public interface IAuthService
    {
        void Register(RegisterDTO registerDTO);
        string? Login(LoginDTO loginDTO);
    }
}
