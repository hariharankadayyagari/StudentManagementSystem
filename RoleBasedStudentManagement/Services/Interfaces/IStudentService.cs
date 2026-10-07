using RoleBasedStudentManagement.DTOs.Student;

namespace RoleBasedStudentManagement.Services.Interfaces
{
    public interface IStudentService
    {
        List<StudentResponseDTO> GetAll();
        StudentResponseDTO? GetById(int id);
        StudentResponseDTO? Update(int id, UpdateStudentDTO updateStudentDTO);
        //bool Delete(int id);

        StudentResponseDTO? GetByUserId(int userId);
    }
}
