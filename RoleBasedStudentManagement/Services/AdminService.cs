using RoleBasedStudentManagement.DTOs.Admin;
using RoleBasedStudentManagement.Services.Interfaces;
using RoleBasedStudentManagement.Repositories.Interfaces;
using RoleBasedStudentManagement.Data;
using RoleBasedStudentManagement.Models;

namespace RoleBasedStudentManagement.Services
{
    public class AdminService:IAdminService
    {
        private readonly IUserRepository _userRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly ApplicationDbContext _context;


        public AdminService(IUserRepository userRepository, IStudentRepository studentRepository, ITeacherRepository teacherRepository, ApplicationDbContext context)
        {
            _userRepository = userRepository;
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
            _context = context;
        }

        public List<UserResponseDTO> GetAllUsers()
        {
            return _context.Users.Select(User => new UserResponseDTO
            {
                Id = User.Id,
                Name = User.Name,
                Email = User.Email,
                Role = User.Role,
            }).ToList();

        }
        public void ChangeRole(int userId, ChangeRoleDTO changeRoleDTO)
        {
            var user = _userRepository.GetById(userId);
            if(user == null)
            {
                throw new Exception("user not exists");
            }
            var newRole = changeRoleDTO.Role.Trim();

            if (newRole != "Student" && newRole != "Teacher" && newRole != "Admin")
            {
                throw new Exception("invalid role");
            }
            using var transaction = _context.Database.BeginTransaction();

            try
            {

                if (newRole == "Teacher")
                {
                    var student = _studentRepository.GetByUserId(userId);

                    if(student != null)
                    {
                        _context.Students.Remove(student);
                    }

                    user.Role = "Teacher";
                    _userRepository.Update(user);

                    var teacher = new Models.Teacher
                    {
                        UserId = userId,
                        Subject = changeRoleDTO.Subject,
                        Department = changeRoleDTO.Department
                    };
                    _teacherRepository.Create(teacher);
                }
                if (newRole == "Student")
                {
                    var teacher = _teacherRepository.GetByUserId(userId);

                    if (teacher != null)
                    {
                        _context.Teachers.Remove(teacher);
                    }

                    user.Role = "Student";
                    _userRepository.Update(user);

                    var student = new Models.Student
                    {
                        UserId = userId,
                        Course = changeRoleDTO.Course,
                        Year = changeRoleDTO.Year
                    };
                    _studentRepository.Create(student);
                }

                if (newRole == "Admin")
                {
                    var student = _studentRepository.GetByUserId(userId);


                    var teacher = _teacherRepository.GetByUserId(userId);

                    if (student != null)
                    {
                        _context.Students.Remove(student);
                    }

                    if (teacher != null)
                    {
                        _context.Teachers.Remove(teacher);
                    }


                    user.Role = "Admin";

                    _userRepository.Update(user);
                }
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        } 
    }
}
