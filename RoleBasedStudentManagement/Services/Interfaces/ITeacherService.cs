using RoleBasedStudentManagement.DTOs.Teacher;


namespace RoleBasedStudentManagement.Services.Interfaces
{
    public interface ITeacherService
    {
        List<TeacherResponseDTO> GetAll();
        TeacherResponseDTO? GetById(int id);
        TeacherResponseDTO? Update(int id, UpdateTeacherDTO updateTeacherDTO);

        TeacherResponseDTO? GetByUserId(int userId);
        //bool Delete(int id);
    }
}
