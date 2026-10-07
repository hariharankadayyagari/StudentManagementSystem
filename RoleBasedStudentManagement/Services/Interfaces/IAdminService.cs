using RoleBasedStudentManagement.DTOs.Admin;
using RoleBasedStudentManagement.Models;

namespace RoleBasedStudentManagement.Services.Interfaces
{
    public interface IAdminService
    {
        void ChangeRole(int userId, ChangeRoleDTO changeRoleDTO);
        List<UserResponseDTO> GetAllUsers();
    }
}
