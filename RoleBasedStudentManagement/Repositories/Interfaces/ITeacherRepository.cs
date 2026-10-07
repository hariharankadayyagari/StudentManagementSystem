using RoleBasedStudentManagement.Models;

namespace RoleBasedStudentManagement.Repositories.Interfaces
{
    public interface ITeacherRepository
    {
        List<Teacher> GetAll();
        Teacher? GetById(int id);
        Teacher? GetByUserId(int userId);
        Teacher Create(Teacher teacher);
        Teacher Update(Teacher teacher);
       // bool Delete(int id);

    }
}
