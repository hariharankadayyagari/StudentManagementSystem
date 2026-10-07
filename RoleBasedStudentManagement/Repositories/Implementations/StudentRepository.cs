using RoleBasedStudentManagement.Data;
using RoleBasedStudentManagement.Models;
using RoleBasedStudentManagement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace RoleBasedStudentManagement.Repositories.Implementations
{
    public class StudentRepository: IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;


        }
        public List<Student> GetAll()
        {
            return _context.Students.Include(s => s.User).ToList();
        }
        public Student? GetById(int id)
        {
            return _context.Students.Include(s=>s.User).FirstOrDefault(s => s.Id == id);
        }

        public Student? GetByUserId(int userId)
        {
            return _context.Students.Include(s=>s.User).FirstOrDefault(s=> s.UserId== userId);
        }

        public Student Create(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
            return student;
        }

        public Student Update(Student student)
        {
            _context.Students.Update(student);
            _context.SaveChanges();

            return student;
        }
    }
}
