using RoleBasedStudentManagement.Models;
using RoleBasedStudentManagement.Data;
using RoleBasedStudentManagement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace RoleBasedStudentManagement.Repositories.Implementations
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ApplicationDbContext _context;

        public TeacherRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Teacher> GetAll()
        {
            return _context.Teachers.Include(t=>t.User).ToList();
        }

        public Teacher? GetById(int id)
        {
            return _context.Teachers.Include(t=>t.User).FirstOrDefault(t => t.Id == id);
        }

        public Teacher? GetByUserId(int userId)
        {
            return _context.Teachers.Include(t => t.User).FirstOrDefault(t => t.UserId == userId);
        }
        public Teacher Create(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            _context.SaveChanges();
            return teacher;
        }
        public Teacher Update(Teacher teacher)
        {
            _context.Teachers.Update(teacher);
            _context.SaveChanges();
            return teacher;
        }
       
    }
}
