using RoleBasedStudentManagement.Models;


namespace RoleBasedStudentManagement.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User? GetByEmail(string email);
        User? GetById(int  id);
        User Create(User user);
        User Update(User user);

    }
}
