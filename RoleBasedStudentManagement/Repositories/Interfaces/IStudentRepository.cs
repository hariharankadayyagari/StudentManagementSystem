using RoleBasedStudentManagement.Models;

namespace RoleBasedStudentManagement.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        List<Student> GetAll();
        Student? GetById(int id);
        Student? GetByUserId(int userId);
        Student Create(Student student);
        Student Update(Student student);
        //bool Delete(int id);
    }
}
